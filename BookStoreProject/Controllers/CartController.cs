using BusinessLayer.Intrface;
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
    public class CartController : ControllerBase
    {
        ICartBL cartBL;
        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }
        [Authorize(Roles = Role.User)]
        [HttpPost("AddCart")]
        public ActionResult AddCart(CartModel cartModel)
        {
            try
            {

                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

                var result = this.cartBL.AddCart(cartModel, UserId);

                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Cart Added succssfully !!", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Cart Not Added" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpPut("UpdateCart")]
        public ActionResult UpdateCart(CartModel cartModel)
        {
            try
            {

                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

                var result = this.cartBL.UpdateCart(cartModel, UserId);

                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Cart Updated succssfully !!", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Cart Not Updated" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpDelete("RemoveCart")]
        public IActionResult RemoveCart(int CartId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

                var result = this.cartBL.RemoveCart(CartId, UserId);

                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Cart Remove succssfully !!", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Cart Not Remove" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpGet("GetAllCart")]
        public IActionResult GetAllCart()
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

                var result = this.cartBL.GetAllBookFromCart(UserId);

                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Get All Book From Cart succssfully !!", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "not succssfully" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
