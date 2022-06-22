using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Models
{
   public class CartResponseModel
    {
        public int UserId { get; set; }
        public int CartId { get; set; }
        public int Quantity_Of_Book { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string Description { get; set; }
        public int Book_Quantity { get; set; }
        public string Book_Image { get; set; }
        public int Orignal_Price { get; set; }
        public int Discount_Price { get; set; }
        public float Rating { get; set; }
        public int Total_Count_Of_rating { get; set; }
    }
}
