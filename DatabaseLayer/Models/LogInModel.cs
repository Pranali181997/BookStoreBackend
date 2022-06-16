using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Models
{
    public class LogInModel
    {
        public string Email {  get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
    }
}
