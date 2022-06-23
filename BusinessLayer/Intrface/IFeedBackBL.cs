using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Intrface
{
   public interface IFeedBackBL
    {
        public FeedBackModel AddFeedBack(FeedBackModel feedBackModel, int UserId);
        public List<FeedBackResModel> GetAllFeedBack(int userId, int BookId);
    }
}
