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
    public class FeedBackRL : IFeedBackRL
    {
        readonly SqlConnection connection = new SqlConnection();
        readonly string sqlConnectString;
        public FeedBackRL(IConfiguration configuration)
        {

            sqlConnectString = configuration.GetConnectionString("BookStore");
            connection.ConnectionString = sqlConnectString;
        }
        public FeedBackModel AddFeedBack(FeedBackModel feedBackModel, int UserId)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("SPForAddFeedBack", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@rating", feedBackModel.rating);
            cmd.Parameters.AddWithValue("@FeedBack", feedBackModel.FeedBack);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.Parameters.AddWithValue("@BookId", feedBackModel.BookId);
            var result = cmd.ExecuteNonQuery();
            if(result != 0)
            {
                connection.Close();
                return feedBackModel;
            }
            else
            {
                return null;
            }
        }
        public List<FeedBackResModel> GetAllFeedBack(int userId,int BookId)
        {
            try
            {
                List<FeedBackResModel> feedBackModellist = new List<FeedBackResModel>();
                connection.Open();
                SqlCommand cmd = new SqlCommand("SPForGetAllFeedback", connection);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("BookId", BookId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        FeedBackResModel feedBackModel = new FeedBackResModel();
                        FeedBackResModel temp;
                        temp = ReadData(feedBackModel, reader);
                        feedBackModellist.Add(temp);
                    }
                    connection.Close();
                    return feedBackModellist;
                }
                else
                {
                    connection.Close();
                    return feedBackModellist;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public FeedBackResModel ReadData(FeedBackResModel feedBackModel,SqlDataReader reader)
        {
           
            feedBackModel.rating = Convert.ToInt32(reader["rating"]);
            feedBackModel.FeedBack = Convert.ToString(reader["FeedBack"]);
            feedBackModel.BookId = Convert.ToInt32(reader["BookId"]);
            feedBackModel.FullName = Convert.ToString(reader["FullName"]);
            return feedBackModel;
        }
    }
}
