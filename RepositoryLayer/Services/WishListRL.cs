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
    public class WishListRL : IWishListRL
    {
        readonly SqlConnection connection = new SqlConnection();
        readonly string sqlConnectString;
        public WishListRL(IConfiguration configuration)
        {

            sqlConnectString = configuration.GetConnectionString("BookStore");
            connection.ConnectionString = sqlConnectString;
        }
        public string AddToWishList(int BookId, int UserId)
        {
            try
            {

                connection.Open();
                SqlCommand cmd = new SqlCommand("SpForAddToWishList", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookId", BookId);
                cmd.Parameters.AddWithValue("@UserId", UserId);


                var result = cmd.ExecuteNonQuery();
                connection.Close();
                if (result != 0)
                {
                    return "successfully Book Added to WishList ";
                }
                else
                {
                    return "Failed to add to the wishList";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string RemoveFromWishList(int WishListId, int UserId)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SP_Remove_FromWishList", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@WishListId", WishListId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                var result = cmd.ExecuteNonQuery();
                if (result != 0)
                {
                    return " Removed from WishList Successfully";
                }
                else
                {
                    return "Failed to Remove item from WishList";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<WishLIstRsponseModel> GetAllWishList(int userId)
        {
            try
            {
                connection.Open();
                List<WishLIstRsponseModel> wishListResponse = new List<WishLIstRsponseModel>();
                SqlCommand cmd = new SqlCommand("SP_GetAll_FromWishList", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserId", userId);
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        WishLIstRsponseModel wishList = new WishLIstRsponseModel();
                        WishLIstRsponseModel temp;
                        temp = ReadData(wishList, rdr);
                        wishListResponse.Add(temp);
                    }

                    return wishListResponse;
                }
                else
                {
                    connection.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public WishLIstRsponseModel ReadData(WishLIstRsponseModel wishList, SqlDataReader rdr)
        {
            wishList.BookId = Convert.ToInt32(rdr["BookId"]);
            wishList.UserId = Convert.ToInt32(rdr["UserId"]);
            wishList.WishListId = Convert.ToInt32(rdr["WishListId"]);
            wishList.BookName = Convert.ToString(rdr["BookName"]);
            wishList.AuthorName = Convert.ToString(rdr["AuthorName"]);
            wishList.Book_Image = Convert.ToString(rdr["Book_Image"]);
            wishList.Description = Convert.ToString(rdr["Description"] );
            return wishList;
        }
    }

}
