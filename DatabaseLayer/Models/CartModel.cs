﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Models
{
    public class CartModel
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int CartId { get; set; }
        public int Quantity_Of_Book  { get; set; }
    }
}
