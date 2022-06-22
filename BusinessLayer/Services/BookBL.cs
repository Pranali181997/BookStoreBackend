using BusinessLayer.Intrface;
using DatabaseLayer.Models;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class BookBL:IBookBL
    {
        IBookRL bookRL;
        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }
        public BookModel AddBook(BookModel bookModel)
        {
            return this.bookRL.AddBook(bookModel);
        }
        public BookModel UpdateBook(BookModel bookModel)
        {
            return this.bookRL.UpdateBook(bookModel);
        }
        public string DeleteBook(int bookId)
        {
            return this.bookRL.DeleteBook(bookId);
        }
        public List<BookModel> GetBook()
        {
            try
            {
                return this.bookRL.GetBook();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public BookModel GetBookByBookId(int bookId)
        {
            try
            {
                return this.bookRL.GetBookByBookId(bookId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}

