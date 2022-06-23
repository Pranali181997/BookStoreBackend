using BusinessLayer.Intrface;
using DatabaseLayer.Models;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
     public class FeedBackBL:IFeedBackBL
    {
        IFeedBackRL IFeedBackRL;
        public FeedBackBL(IFeedBackRL IFeedBackRL)
        {
            this.IFeedBackRL = IFeedBackRL;
        }
        public FeedBackModel AddFeedBack(FeedBackModel feedBackModel, int UserId)
        {
            try
            {
                return this.IFeedBackRL.AddFeedBack(feedBackModel, UserId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<FeedBackResModel> GetAllFeedBack(int userId, int BookId)
        {
            try
            {
                return this.IFeedBackRL.GetAllFeedBack(userId,BookId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
