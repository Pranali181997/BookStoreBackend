using BusinessLayer.Intrface;
using DatabaseLayer.Models;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
   public class WIshListBL:IWishListBL
    {
        IWishListRL wishlistRL;
        public WIshListBL(IWishListRL wishlistRL)
        {
            this.wishlistRL = wishlistRL;
        }
        public string AddToWishList(int bookId, int userId)
        {
            try
            {
                return this.wishlistRL.AddToWishList(bookId, userId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string RemoveFromWishList(int WishListId, int UserId)
        {
            try
            {
                return this.wishlistRL.RemoveFromWishList(WishListId, UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<WishLIstRsponseModel> GetAllWishList(int userId)
        {
            try
            {
                return this.wishlistRL.GetAllWishList(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
