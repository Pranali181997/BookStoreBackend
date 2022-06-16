using BusinessLayer.Intrface;
using DatabaseLayer.Models;
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
    public class UserController : ControllerBase
    {
        IUserBL userBL;
     public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost("Register")]
        public ActionResult UserRegistration( UserModel userModel)
        {
            try
            {
                var result = this.userBL.UserRegistration(userModel);
                if(result != null)
                {
                    return this.Ok(new { success = true, message = "User Added succssfully !!",data=result});
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "User Not Added" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { success = false, message = ex.Message});
            }
        }
        [HttpPost("LogIn")]
        public ActionResult Login(string Email, string Password)
        {

            try
            {
                var result = this.userBL.LogIn(Email, Password);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "User LogIn succssfully !!", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "LogIn not successfull" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}
