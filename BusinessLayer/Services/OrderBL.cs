using BusinessLayer.Intrface;
using DatabaseLayer.Models;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class OrderBL:IOrderBL
    {
        IOrderRL orderRL;
        public OrderBL(IOrderRL orderRL)
        {
            this.orderRL = orderRL;
        }
        public OrderModel AddOrder(OrderModel orderModel, int UserId)
        {
            try
            {
                return this.orderRL.AddOrder(orderModel, UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<OrderModel> GetAllOrders(int UserId)
        {
            try
            {
                return this.orderRL.GetAllOrders(UserId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string removeOrder(int UserId, int OrderId)
        {
            try
            {
                return this.orderRL.removeOrder(UserId, OrderId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
