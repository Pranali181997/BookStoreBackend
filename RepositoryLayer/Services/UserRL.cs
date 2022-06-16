using DatabaseLayer.Models;
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
    public class UserRL:IUserRL
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
                if(result != 0)
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
                if(reader.HasRows)
                {
                    while(reader.Read())
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
        private string GenerateJWTToken(string Email,int UserId)
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
        }
}
