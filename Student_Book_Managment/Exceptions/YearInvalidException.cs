using System;
using System.Collections.Generic;
using System.Text;

namespace Student_Book_Managment.Exceptions
{
    internal class YearInvalidException : AppException
    {
        public YearInvalidException(string? msg = null) : base(msg ?? $"Year must be between 1 and {DateTime.Now.Year}.") {}
    }
}
