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
    public class AdminRL:IAdminRL
    {
        readonly SqlConnection connection = new SqlConnection();
        readonly string sqlConnectString;
        public AdminRL(IConfiguration configuration)
        {
            sqlConnectString = configuration.GetConnectionString("BookStore");
            connection.ConnectionString = sqlConnectString;
        }
        public string AdminLogIn(string EmailId,String Password)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SPForLogIn", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AdminEmailId", EmailId);
                cmd.Parameters.AddWithValue("@AdminPassword", Password);
                SqlDataReader reader = cmd.ExecuteReader();
              
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        EmailId = Convert.ToString(reader["AdminEmailId"]);
                        Password = Convert.ToString(reader["AdminPassword"]);
                        var AdminId = Convert.ToInt32(reader["AdminId"]);

                        var Token = this.GenerateJWTToken(EmailId, AdminId);
                        return $"{ Token}";
                    }
                    connection.Close();
                    return "LogInSuccessfully";
                }
                else
                {
                   
                    return "logInNotSuccessfull";
                }  
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        private string GenerateJWTToken(string Email, int AdminId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Email", Email),
                    new Claim(ClaimTypes.Role,"Admin"),
                    new Claim("AdminId",AdminId.ToString()),
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
