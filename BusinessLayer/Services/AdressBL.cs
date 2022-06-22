using BusinessLayer.Intrface;
using DatabaseLayer.Models;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AdressBL : IAdressBL
    {
        IAdressRL adressRL;
        public AdressBL(IAdressRL adressRL)
        {
            this.adressRL = adressRL;
        }
        public AdressModel AddAdress(AdressModel adressModel, int UserId)
        {
            try
            {
                return this.adressRL.AddAdress(adressModel,UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DeleteAdress(int AdressId, int UserId)
        {
            try
            {
                return this.adressRL.DeleteAdress(AdressId, UserId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<AdressModel> GetAdressByUserId(int UserId)
        {
            try
            {
                return this.adressRL.GetAdressByUserId(UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public AdressModel UpdateAddress(AdressModel adressModel, int UserId)
        {
            try
            {
                return this.adressRL.UpdateAddress(adressModel, UserId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
