using BusinessLayer.Intrface;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
  public class AdminBL:IAdminBL
    {
        IAdminRL adminRL;
       public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }
        public string AdminLogIn(string EmailId, String Password)
        {
            try
            {
                return this.adminRL.AdminLogIn(EmailId, Password);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
