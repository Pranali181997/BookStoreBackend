using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Intrface
{
    public interface IBookBL
    {
        public BookModel AddBook(BookModel bookModel);
    }
}
