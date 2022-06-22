using BusinessLayer.Intrface;
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
    public class WishListController : ControllerBase
    {
        IWishListBL wishListBL;
        public WishListController(IWishListBL wishListBL)
        {
            this.wishListBL = wishListBL;
        }
        [Authorize(Roles = Role.User)]
        [HttpPost("AddCart")]
        public ActionResult AddToWishList(int BookId)
        {
            try
            {

                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

                var result = this.wishListBL.AddToWishList(BookId,UserId);

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
        [HttpDelete("Remove")]
        public IActionResult RemoveFromWishList(int wishListId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);

                var result = this.wishListBL.AddToWishList(wishListId, UserId);
                if (result != null)
                {
                    return Ok(new { success = true, message = "Removed from WishList sucessfully" });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to Remove from WishList" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpGet("GetAll")]
        public IActionResult GetAllWishList()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var res = wishListBL.GetAllWishList(userId);
                if (res != null)
                {
                    return Ok(new { success = true, message = "Get All WishList sucessfull", data = res });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to GetAll WishList" });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}
 