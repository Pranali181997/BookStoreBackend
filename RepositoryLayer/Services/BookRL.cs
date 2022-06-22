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
    public class BookRL : IBookRL
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
                //cmd.Parameters.AddWithValue("@AdminId", AdminId);

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
        public BookModel UpdateBook(BookModel bookModel)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SP_Update_Book", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BookId", bookModel.BookId);
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
        public string DeleteBook(int bookId)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SP_Delete_Book", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BookId", bookId);
                var result = cmd.ExecuteNonQuery();
                connection.Close();
                if (result != 0)
                {
                    return "Book deleted successfully";
                }
                else
                {
                    return "failed to delte the book";
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<BookModel> GetBook()
        {
            try
            {
                List<BookModel> bookModel = new List<BookModel>();
                connection.Open();
                SqlCommand cmd = new SqlCommand("SP_GetAll_Books", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        BookModel book = new BookModel();
                        BookModel temp;
                        temp = ReadData(book, reader);
                        bookModel.Add(temp);
                    }
                    connection.Close();
                    return bookModel;
                }
                else
                {
                    connection.Close();
                    return bookModel;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
        public BookModel ReadData(BookModel bookModel, SqlDataReader reader)
        {
            BookModel book = new BookModel();
            book.BookId = Convert.ToInt32(reader["BookId"]);
            book.BookName = Convert.ToString(reader["BookName"]);
            book.AuthorName = Convert.ToString(reader["AuthorName"]);
            book.Book_Image = Convert.ToString(reader["Book_Image"]);
            book.Book_Quantity = Convert.ToInt32(reader["Book_Quantity"]);
            book.Description = Convert.ToString(reader["Description"]);
            book.Discount_Price = Convert.ToInt32(reader["Discount_Price"]);
            book.Rating = Convert.ToInt32(reader["Rating"]);
            book.Total_Count_Of_rating = Convert.ToInt32(reader["Total_Count_Of_Rating"]);
            return book;
        }
        public BookModel GetBookByBookId(int bookId)
        {
            try
            {
                BookModel bookModel = new BookModel();
                connection.Open();
                SqlCommand cmd = new SqlCommand("SP_GetBook_ById", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@BookId", bookId);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        bookModel = ReadData(bookModel, reader);
                    }                   
                    return bookModel;
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
    }
}
