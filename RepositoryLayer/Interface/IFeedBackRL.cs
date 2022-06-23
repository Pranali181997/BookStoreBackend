using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IFeedBackRL
    {
        public FeedBackModel AddFeedBack(FeedBackModel feedBackModel, int UserId);
        public List<FeedBackResModel> GetAllFeedBack(int userId, int BookId);
    }
}
