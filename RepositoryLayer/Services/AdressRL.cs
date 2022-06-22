using DatabaseLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AdressRL : IAdressRL
    {
        readonly SqlConnection connection = new SqlConnection();
        readonly string sqlConnectString;
        public AdressRL(IConfiguration configuration)
        {
            sqlConnectString = configuration.GetConnectionString("BookStore");
            connection.ConnectionString = sqlConnectString;
        }
        public AdressModel AddAdress(AdressModel adressModel,int UserId)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SPForAddAddress", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Address", adressModel.Address);
                cmd.Parameters.AddWithValue("@City", adressModel.City);
                cmd.Parameters.AddWithValue("@State", adressModel.State);
                cmd.Parameters.AddWithValue("@AdTypeId", adressModel.AdTypeId);
                cmd.Parameters.AddWithValue("@UserId", UserId);

                var result = cmd.ExecuteNonQuery();
                if (result != 0)
                {
                    connection.Close();
                    return adressModel;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        public string DeleteAdress(int AdressId ,int UserId)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SPDeleteAddress", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AddressId", AdressId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                var result = cmd.ExecuteNonQuery();
                if(result != 0)
                {
                    connection.Close();
                    return $"Address deleted successfully with UserId {UserId} and AdressId {AdressId}";
                }
                else
                {
                    return "Address is not deleted";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<AdressModel> GetAdressByUserId(int UserId)
        {
            try
            {
                List<AdressModel> adressModelslist = new List<AdressModel>();
                connection.Open();
                SqlCommand cmd = new SqlCommand("spForGetAddressByUserId", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AdressModel adressModel = new AdressModel();
                        AdressModel temp;
                        temp = ReadData(adressModel, reader);
                        adressModelslist.Add(temp);
                    }
                    connection.Close();
                    return adressModelslist;
                }
                else
                {                  
                    return adressModelslist;                   
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public AdressModel ReadData(AdressModel adressModel,SqlDataReader reader)
        {
            AdressModel adress = new AdressModel();
            adress.Address = Convert.ToString(reader["adress"]);
            adress.City = Convert.ToString(reader["City"]);
            adress.State = Convert.ToString(reader["State"]);
            adress.UserId = Convert.ToInt32(reader["UserId"]);
            adress.AdTypeId = Convert.ToInt32(reader["AdTypeId"]);          
            return adress;
        }
        public AdressModel UpdateAddress(AdressModel adressModel, int UserId)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SPforUpdateAddress", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Address", adressModel.Address);
                cmd.Parameters.AddWithValue("@City", adressModel.City);
                cmd.Parameters.AddWithValue("@State", adressModel.State);
                cmd.Parameters.AddWithValue("@AdTypeId", adressModel.AdTypeId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                
                cmd.Parameters.AddWithValue("@AddressId", adressModel.AddressId);

                var result = cmd.ExecuteNonQuery();
                if(result != 0)
                {
                    connection.Close();
                    return adressModel;
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
    }
}
