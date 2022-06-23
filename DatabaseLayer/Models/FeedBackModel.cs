using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Models
{
   public class FeedBackModel
    {
        public float rating { set; get; }
        public string FeedBack { set; get; }
        public int BookId { set; get; }      
    }
}
