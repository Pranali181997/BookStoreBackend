using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Intrface
{
    public interface IAdminBL
    {
        public string AdminLogIn(string EmailId, String Password);
    }
}
