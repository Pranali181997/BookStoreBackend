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
    public class BookRL:IBookRL
    {
        readonly SqlConnection connection = new SqlConnection();
        readonly string sqlConnectString;
        public BookRL(IConfiguration configuration)
        {
            sqlConnectString = configuration.GetConnectionString("BookStore");
            connection.ConnectionString = sqlConnectString;
        }
        public BookModel AddBook(BookModel bookModel)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("spAddBook", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookName", bookModel.BookName);
                cmd.Parameters.AddWithValue("@AuthorName", bookModel.AuthorName);
                cmd.Parameters.AddWithValue("@Description", bookModel.Description);
                cmd.Parameters.AddWithValue("@Book_Quantity", bookModel.Book_Quantity);
                cmd.Parameters.AddWithValue("@Book_Image", bookModel.Book_Image);
                cmd.Parameters.AddWithValue("@Orignal_Price", bookModel.Orignal_Price);
                cmd.Parameters.AddWithValue("@Discount_Price", bookModel.Discount_Price);
                cmd.Parameters.AddWithValue("@Rating", bookModel.Rating);
                cmd.Parameters.AddWithValue("@Total_Count_Of_rating", bookModel.Total_Count_Of_rating);

                var result = cmd.ExecuteNonQuery();
                connection.Close();
                if (result != 0)
                {
                    return bookModel;
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
