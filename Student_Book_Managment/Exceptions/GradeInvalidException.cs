using System;
using System.Collections.Generic;
using System.Text;

namespace Student_Book_Managment.Exceptions
{
    internal class GradeInvalidException : AppException
    {
        public GradeInvalidException(string msg = "Argument isn't a valid grade.") : base(msg) {}
    }
}
