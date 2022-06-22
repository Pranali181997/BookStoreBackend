using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICartRL
    {
        public CartModel AddCart(CartModel cartModel, int UserId);
        public CartModel UpdateCart(CartModel cartModel, int UserId);
        public string RemoveCart(int CartId, int UserId);
        public List<CartResponseModel> GetAllBookFromCart(int UserId);
    }
}
