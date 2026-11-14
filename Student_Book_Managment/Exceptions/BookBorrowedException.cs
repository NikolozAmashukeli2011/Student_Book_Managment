using System;
using System.Collections.Generic;
using System.Text;

namespace Student_Book_Managment.Exceptions
{
    internal class BookBorrowedException : AppException
    {
        public BookBorrowedException(string msg = "Book is already borrowed."): base(msg) { }
    }
}
