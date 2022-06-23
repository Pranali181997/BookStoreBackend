using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Intrface
{
   public interface IOrderBL
    {
        public OrderModel AddOrder(OrderModel orderModel, int UserId);
        public List<OrderModel> GetAllOrders(int UserId);
        public string removeOrder(int UserId, int OrderId);
    }
}
