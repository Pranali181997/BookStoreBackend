using DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IBookRL
    {
        public BookModel AddBook(BookModel bookModel);
        public BookModel UpdateBook(BookModel bookModel);
        public string DeleteBook(int bookId);
        public List<BookModel> GetBook();
        public BookModel GetBookByBookId(int bookId);
    }
}
