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
    public class FeekBackController : ControllerBase
    {
        IFeedBackBL feedBackBL;

        public FeekBackController(IFeedBackBL feedBackBL)
        {
            this.feedBackBL = feedBackBL;
        }
        [Authorize(Roles =Role.User)]
        [HttpPost("AddFeedBack")]
        public ActionResult AddFeedBack(FeedBackModel feedBackModel)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == "UserId").Value);
                var result = this.feedBackBL.AddFeedBack(feedBackModel, UserId);
                if(result != null)
                {
                    return this.Ok(new { success = true, message = "FeedBack Added Successfully", data = result });
                }
                else
                {
                    return this.Ok(new { success = false, message = "FeedBack not Added Successfully"});
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize(Roles =Role.User)]
        [HttpGet("GetAllFeedBack")]
        public IActionResult GetAllFeedBack(int BookId)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == "UserId").Value);
                var result = this.feedBackBL.GetAllFeedBack(UserId,BookId);
                if(result != null)
                {
                    return this.Ok(new { success = true, message = "Got the all FeedBack successfully", data = result });
                }
                else
                {
                    return this.Ok(new { success = false, message = "not getting all Feedbacks" });
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
