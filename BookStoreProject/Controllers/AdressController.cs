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
    public class AdressController : ControllerBase
    {
        IAdressBL adressBL;
        public AdressController(IAdressBL adressBL)
        {
            this.adressBL = adressBL;
        }
        [Authorize(Roles = Role.User)]
        [HttpPost("Add_Address")]
        public ActionResult AddAdress(AdressModel adressModel)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == "UserId").Value);
                var result = this.adressBL.AddAdress(adressModel, UserId);
                if(result != null)
                {
                    return this.Ok(new { sucess = true, message = "Address Added successfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { sucess = false, message = "Address not Added successfully", data = result });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [Authorize(Roles = Role.User)]
        [HttpDelete("Delete_Address")]
        public IActionResult DeleteAdress(int AdressId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == "UserId").Value);
                var result = this.adressBL.DeleteAdress(AdressId, UserId);
                if (result != null)
                {
                    return this.Ok(new { sucess = true, message = "Address Deleted successfully ", data = result });
                }
                else
                {
                    return this.BadRequest(new { sucess = false, message = "Address not deleted successfully", data = result });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            } 
        }
        [Authorize(Roles = Role.User)]
        [HttpGet("Get_All_Address")]
        public IActionResult Get_All_Addres()
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == "UserId").Value);
                var result = this.adressBL.GetAdressByUserId( UserId);
                if (result != null)
                {
                    return this.Ok(new { sucess = true, message = "Get_All_Address successfully ", data = result });
                }
                else
                {
                    return this.BadRequest(new { sucess = false, message = "Get_All_Address not got successfully"});
                }            
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize(Roles =Role.User)]
        [HttpPut("Update Address")]
        public IActionResult UpdateAddress(AdressModel adressModel)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == "UserId").Value);
                var result = this.adressBL.UpdateAddress(adressModel, UserId);
                if (result != null)
                {
                    return this.Ok(new { sucess = true, message = "Address Updated successfully ", data = result });
                }
                else
                {
                    return this.BadRequest(new { sucess = false, message = "Address not updated" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
