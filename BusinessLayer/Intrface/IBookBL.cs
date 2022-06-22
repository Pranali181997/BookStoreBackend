using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Intrface
{
    public interface IBookBL
    {
        public BookModel AddBook(BookModel bookModel);
        public BookModel UpdateBook(BookModel bookModel);
        public string DeleteBook(int bookId);
        public List<BookModel> GetBook();
        public BookModel GetBookByBookId(int bookId);
    }
}
