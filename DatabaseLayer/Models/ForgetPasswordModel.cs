using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Models
{
   public class ForgetPasswordModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
    }
}
