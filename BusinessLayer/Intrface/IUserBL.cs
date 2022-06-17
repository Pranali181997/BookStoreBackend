using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Intrface
{
   public interface IUserBL
    {
        public UserModel UserRegistration(UserModel userModel);
        public LogInModel LogIn(string Email, string Password);
        public bool ForgetPassword(ForgetPasswordModel forgetPassword);
        public string ResetPassword(string Email, string Password, string newPassword);
    }
}
