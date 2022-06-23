using BusinessLayer.Intrface;
using BusinessLayer.Services;
using DatabaseLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        IOrderBL orderBL;
        public OrderController(IOrderBL orderBL)
        {
            this.orderBL = orderBL;
        }
        [Authorize(Roles = Role.User)]
        [HttpPost("AddOrder")]
        public ActionResult AddOrder(OrderModel orderModel)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == "UserId").Value);
                var result = this.orderBL.AddOrder(orderModel, UserId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Order Placed succssfully !!", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Order Not Added" });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpPost("GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == "UserId").Value);
                var result = this.orderBL.GetAllOrders(UserId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Get All Orders succssfully !!", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Order Not got" });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [Authorize(Roles =Role.User)]
        [HttpDelete("Remove Order")]
        public IActionResult removeOrder(int OrderId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == "UserId").Value);
                var result = this.orderBL.removeOrder(UserId, OrderId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Remove Order succssfully !!", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Order Not Remove" });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

}
