using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public UserModel UserRegistration(UserModel userModel);
        public LogInModel LogIn(string Email, string Password);
    }
}
