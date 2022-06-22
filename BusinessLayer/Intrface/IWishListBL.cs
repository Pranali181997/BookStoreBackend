using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Intrface
{
   public interface IWishListBL
    {
        public string AddToWishList(int bookId, int userId);
        public string RemoveFromWishList(int WishListId, int UserId);
        public List<WishLIstRsponseModel> GetAllWishList(int userId);
    }
}
