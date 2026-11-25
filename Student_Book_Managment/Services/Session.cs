using Student_Managment.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Student_Book_Managment.Services
{
    internal static class Session
    {
        public static Student? CurrentStudent = null; // static class storing the current student, which is student whose id matches the student (Roles.Student) users inputted id.
    }
}
