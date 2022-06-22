using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Intrface
{
   public interface IAdressBL
    {
        public AdressModel AddAdress(AdressModel adressModel, int UserId);
        public string DeleteAdress(int AdressId, int UserId);
        public List<AdressModel> GetAdressByUserId(int UserId);
        public AdressModel UpdateAddress(AdressModel adressModel, int UserId);
    }
}
