using System;
using System.Collections.Generic;
using System.Text;

namespace Student_Book_Managment.Exceptions
{
    internal class YearInvalidException : AppException
    {
        public YearInvalidException(string msg = "Year must be 1 through 2026.") : base(msg) {}
    }
}
