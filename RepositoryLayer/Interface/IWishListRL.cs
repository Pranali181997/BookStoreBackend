using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IWishListRL
    {
        public string AddToWishList(int bookId, int userId);
        public string RemoveFromWishList(int WishListId, int UserId);
        public List<WishLIstRsponseModel> GetAllWishList(int userId);
    }
}
