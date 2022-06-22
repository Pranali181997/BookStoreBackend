using BusinessLayer.Intrface;
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
    public class AdminController : ControllerBase
    {
        IAdminBL AdminBL;
        public AdminController(IAdminBL AdminBL)
        {
            this.AdminBL = AdminBL;
        }
        [HttpPost("LogIn")]
        public ActionResult AdminLogIn(string Email,String Password)
        {
            try
            {       
                
                var result = this.AdminBL.AdminLogIn(Email, Password);
                if(result != null)
                {
                    return this.Ok(new { success = true, message = "LogIn Successfull", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "LogIn not Successfull", data = result });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
