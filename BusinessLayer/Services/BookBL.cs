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
    }
}
