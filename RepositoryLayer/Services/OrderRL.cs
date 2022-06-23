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
    public class OrderRL : IOrderRL
    {
        readonly SqlConnection connection = new SqlConnection();
        readonly string sqlConnectString;
        public OrderRL(IConfiguration configuration)
        {

            sqlConnectString = configuration.GetConnectionString("BookStore");
            connection.ConnectionString = sqlConnectString;
        }
        public OrderModel AddOrder(OrderModel orderModel, int UserId)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("AddOrder", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@BookId", orderModel.BookId);
            cmd.Parameters.AddWithValue("@AddressId", orderModel.AddressId);
            cmd.Parameters.AddWithValue("@UserId", UserId);

            var result = Convert.ToInt32(cmd.ExecuteScalar());
            if (result != 2)
            {
                connection.Close();
                return orderModel;
            }
            else
            {
                return null;
            }
        }
        public List<OrderModel> GetAllOrders(int UserId)
        {
            try
            {
                List<OrderModel> orderModels = new List<OrderModel>();
                connection.Open();
                SqlCommand cmd = new SqlCommand("SpGetorder", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrderModel orderModel = new OrderModel();
                        OrderModel temp;
                        temp = ReadData(orderModel, reader);
                        orderModels.Add(temp);
                    }
                    connection.Close();
                    return orderModels;
                }
                else
                {
                    connection.Close();
                    return orderModels;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private OrderModel ReadData(OrderModel orderModel, SqlDataReader reader)
        {
            orderModel.BookId = Convert.ToInt32(reader["BookId"]);
            orderModel.AddressId = Convert.ToInt32(reader["AddressId"]);
            orderModel.Actual_Price = Convert.ToInt32(reader["Actual_Price"]);
            orderModel.Order_Price = Convert.ToInt32(reader["Order_Price"]);
            orderModel.Books_Qty = Convert.ToInt32(reader["Books_Qty"]);
            return orderModel;
        }
        public string removeOrder(int UserId,int OrderId)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SpForRemoveOrder", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserId", UserId);
                cmd.Parameters.AddWithValue("@OrderId", OrderId);
                var result = cmd.ExecuteNonQuery();
                if(result != 0)
                {
                    connection.Close();
                    return $"Order removed for orderId={OrderId} and userId={UserId}";
                }
                else
                {
                    return "not removed";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
