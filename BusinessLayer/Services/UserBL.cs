using BusinessLayer.Intrface;
using DatabaseLayer.Models;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
  public class UserBL:IUserBL
    {
        IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public UserModel UserRegistration(UserModel userModel)
        {
            try
            {
                return this.userRL.UserRegistration(userModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public LogInModel LogIn(string Email, string Password)
        {
            try
            {
                return this.userRL.LogIn(Email, Password);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
