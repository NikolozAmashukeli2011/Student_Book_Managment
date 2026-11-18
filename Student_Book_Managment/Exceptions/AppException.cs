using System;
using System.Collections.Generic;
using System.Text;

namespace Student_Book_Managment.Exceptions
{
    abstract class AppException : Exception
    {
        public DateTime ThrownAt { get; init; } = DateTime.UtcNow;

        public AppException(string message) : base(message) {}
    }
}
