using DatabaseLayer.Models;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        readonly SqlConnection connection = new SqlConnection();
        readonly string sqlConnectString;

        public UserRL(IConfiguration configuration)
        {
            sqlConnectString = configuration.GetConnectionString("BookStore");
            connection.ConnectionString = sqlConnectString;
        }
        public UserModel UserRegistration(UserModel userModel)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("spAddUser", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                var encryptedPassword = EncryptPassword(userModel.Password);
                cmd.Parameters.AddWithValue("@FullName", userModel.FullName);
                cmd.Parameters.AddWithValue("@EmailId", userModel.Email);
                cmd.Parameters.AddWithValue("@Password", encryptedPassword);
                cmd.Parameters.AddWithValue("@MobileNumber", userModel.MobileNumber);

                var result = cmd.ExecuteNonQuery();
                connection.Close();
                if (result != 0)
                {
                    return userModel;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private object EncryptPassword(string password)
        {
            try
            {
                if (string.IsNullOrEmpty(password))
                {
                    return null;
                }
                else
                {
                    byte[] b = Encoding.ASCII.GetBytes(password);
                    string encrypted = Convert.ToBase64String(b);
                    return encrypted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public LogInModel LogIn(string Email, string Password)
        {
            LogInModel login = new LogInModel();
            try
            {

                connection.Open();

                SqlCommand cmd = new SqlCommand("spLoginUser", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmailId", Email);
                var encryptedPassword = EncryptPassword(Password);
                cmd.Parameters.AddWithValue("@Password", encryptedPassword);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        login.Email = Convert.ToString(reader["EmailId"]);
                        login.Password = Convert.ToString(reader["Password"]);
                        login.UserId = Convert.ToInt32(reader["UserId"]);
                    }
                    connection.Close();
                    login.Token = this.GenerateJWTToken(Email, login.UserId);
                    return login;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string GenerateJWTToken(string Email, int UserId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Email", Email),
                    new Claim("UserId",UserId.ToString()),
                    //new Claim("UserName",)
                }),
                Expires = DateTime.UtcNow.AddHours(24),

                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool ForgetPassword(ForgetPasswordModel forgetPassword)
        {
            try
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand("spForgetPassword", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmailId", forgetPassword.Email);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var UserId = Convert.ToInt32(reader["UserId"]);

                        //Add message queue
                        MessageQueue queue;
                        if (MessageQueue.Exists(@".\Private$\FundooQueue"))
                        {
                            queue = new MessageQueue(@".\Private$\FundooQueue");
                        }
                        else
                        {
                            queue = MessageQueue.Create(@".\Private$\FundooQueue");
                        }
                        Message message = new Message();
                        message.Formatter = new BinaryMessageFormatter();
                        message.Body = GenerateJWTToken(forgetPassword.Email, UserId);
                        message.Label = "Forget Password Email";
                        queue.Send(message);
                        Message msg = queue.Receive();
                        msg.Formatter = new BinaryMessageFormatter();
                        EmailServices.SendMail(forgetPassword.Email, msg.Body.ToString());
                        queue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);
                        queue.BeginReceive();
                        queue.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
               
                throw ex;
            }
            return false;
        }
        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            //throw new NotImplementedException();
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailServices.SendMail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
                queue.BeginReceive();
            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode ==
                    MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " +
                        "Queue might be a system queue.");
                }
                // Handle other sources of MessageQueueException.
            }
        }
        //GENERATE TOKEN WITH EMAIL
        public string GenerateToken(string email)
        {
            if (email == null)
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Email",email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string ResetPassword(string Email,string Password,string newPassword)
        {
            try
            {
                if (Password == newPassword)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("spResetPassword", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmailId", Email);
                    var encryptedPassword = EncryptPassword(Password);
                    cmd.Parameters.AddWithValue("@Password", encryptedPassword);

                    var result = cmd.ExecuteNonQuery();
                    if (result != 0)
                    {
                        return "Password change successfully";
                    }
                    else
                    {
                        return "not found";
                    }
                }
                else
                {
                    return "Password should be match";
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
