using BusinessLayer.Intrface;
using DatabaseLayer.Models;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartBL:ICartBL
    {
        ICartRL cartRL;
        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }
        public CartModel AddCart(CartModel cartModel, int UserId)
        {
            try
            {
                return this.cartRL.AddCart(cartModel,UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public CartModel UpdateCart(CartModel cartModel, int UserId)
        {
            try
            {
                return this.cartRL.UpdateCart(cartModel,UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string RemoveCart(int CartId, int UserId)
        {
            try
            {
                return this.cartRL.RemoveCart(CartId, UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<CartResponseModel> GetAllBookFromCart(int UserId)
        {
            try
            {
                return this.cartRL.GetAllBookFromCart( UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
