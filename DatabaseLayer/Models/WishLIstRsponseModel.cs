using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Models
{
  public class WishLIstRsponseModel
    {
        public int UserId { get; set; }
        public int WishListId { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string Description { get; set; }
        public string Book_Image { get; set; }       
    }
}
