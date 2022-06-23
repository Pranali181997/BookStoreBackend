using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public int AddressId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime Order_Date { get; set; }
        public int Books_Qty { get; set; }
        public float Order_Price { get; set; }
        public float Actual_Price { get; set; }

    }
}
