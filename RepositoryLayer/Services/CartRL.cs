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
    public class CartRL : ICartRL
    {

        readonly SqlConnection connection = new SqlConnection();
        readonly string sqlConnectString;
        public CartRL(IConfiguration configuration)
        {
            
            sqlConnectString = configuration.GetConnectionString("BookStore");
            connection.ConnectionString = sqlConnectString;
        }
        public CartModel AddCart(CartModel cartModel, int UserId)
        {
            try
            {
                
                connection.Open();
                SqlCommand cmd = new SqlCommand("spAddCart", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@BookId", cartModel.BookId);
                cmd.Parameters.AddWithValue("@Quantity_Of_Book", cartModel.Quantity_Of_Book);
                var result = cmd.ExecuteNonQuery();
                connection.Close();
                if(result != 0)
                {
                    return cartModel;
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
        public CartModel UpdateCart(CartModel cartModel, int UserId)
        {

            
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SP_UpdateQty_InCart", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@CartId", cartModel.CartId);
                cmd.Parameters.AddWithValue("@Quantity_Of_Book", cartModel.Quantity_Of_Book);
                var result = cmd.ExecuteNonQuery();
                connection.Close();

                if(result != 0)
                {
                    return cartModel;
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
        public string RemoveCart(int CartId,int UserId)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SP_RemoveCart", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@cartId",CartId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                var result = cmd.ExecuteNonQuery();
                if(result != 0)
                {
                    return "book remove from book successfully";
                }
                else
                {
                    return "book can't remove";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<CartResponseModel> GetAllBookFromCart(int UserId)
        {
            try
            {
                connection.Open();
                List<CartResponseModel> cartModel = new List<CartResponseModel>();
                SqlCommand cmd = new SqlCommand("spForGetBookFromCart", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("UserId", UserId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CartResponseModel cart = new CartResponseModel();
                        CartResponseModel temp;
                        temp = ReadData(cart, reader);
                        cartModel.Add(temp);
                    }
                    connection.Close();
                    return cartModel;
                }
                else
                {
                    connection.Close();
                    return cartModel;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private CartResponseModel ReadData(CartResponseModel cart, SqlDataReader reader)
        {
            
            cart.CartId = Convert.ToInt32(reader["CartId"]);
            cart.UserId = Convert.ToInt32(reader["UserId"]);
            cart.Quantity_Of_Book = Convert.ToInt32(reader["Quantity_Of_Book"]);
            cart.BookId = Convert.ToInt32(reader["BookId"]);
            cart.BookName = Convert.ToString(reader["BookName"]);
            cart.AuthorName = Convert.ToString(reader["AuthorName"]);
            cart.Book_Image = Convert.ToString(reader["Book_Image"]);
           
            cart.Description = Convert.ToString(reader["Description"]);
            cart.Discount_Price = Convert.ToInt32(reader["Discount_Price"]);
            cart.Rating = Convert.ToInt32(reader["Rating"]);
            cart.Total_Count_Of_rating = Convert.ToInt32(reader["Total_Count_Of_Rating"]);
            return cart;
        }

    }
}
