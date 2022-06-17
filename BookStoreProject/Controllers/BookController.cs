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
    public class BookController : ControllerBase
    {
        IBookBL bookBL;
        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }
        [HttpPost("Add Book")]
        public ActionResult AddBook(BookModel bookModel)
        {
            {
                try
                {
                    var result = this.bookBL.AddBook(bookModel);
                    if (result != null)
                    {
                        return this.Ok(new { success = true, message = "Book Added succssfully !!", data = result });
                    }
                    else
                    {
                        return this.BadRequest(new { success = false, message = "Book Not Added" });
                    }
                }
                catch (Exception ex)
                {
                    return this.NotFound(new { success = false, message = ex.Message });
                }
            }
        }
    }
}
