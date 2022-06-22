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
    public class BookController : ControllerBase
    {
        IBookBL bookBL;
        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }
        [Authorize(Roles =Role.Admin)]
        [HttpPost("Add Book")]
        public ActionResult AddBook(BookModel bookModel)
        {         
                try
                {
                   //int AdminId = Convert.ToInt32(User.Claims.FirstOrDefault(u => u.Type == "AdminId").Value);
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
        [Authorize(Roles = Role.Admin)]
        [HttpPut("Update_Book")]
        public ActionResult UpdateBook(BookModel bookModel)
        {
                try
                {
                    var result = this.bookBL.UpdateBook(bookModel);
                    if (result != null)
                    {
                        return this.Ok(new { success = true, message = "Book Updated succssfully !!", data = result });
                    }
                    else
                    {
                        return this.BadRequest(new { success = false, message = "Book Not Updated" });
                    }
                }
                catch (Exception ex)
                {
                    return this.NotFound(new { success = false, message = ex.Message });
                }
        }
        [Authorize(Roles = Role.Admin)]
        [HttpDelete("Delete_Book")]
        public ActionResult DeleteBook(int bookId)
        {
            try
            {
                var result = this.bookBL.DeleteBook(bookId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Book Deleted succssfully !!", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Book Not Deleted" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }
        [Authorize(Roles = Role.Admin)]
        [HttpGet("Get_All_Book")]
        public ActionResult GetBook()
        {
            try
            {
                var result = this.bookBL.GetBook();
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Find the below all Book", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed to get the all book" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }
        [Authorize(Roles = Role.Admin)]
        [HttpGet("Get_Book_By_BookId")]
        public IActionResult GetBookByBookId(int bookId)
        {
            try
            {
                var result = this.bookBL.GetBookByBookId(bookId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = $"Book Found For bookId {bookId}", data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Failed to get book" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { success = false, message = ex.Message });
            }
        }
    }
}
