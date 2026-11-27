using Student_Book_Managment.Attributes;
using Student_Book_Managment.Exceptions;
using Student_Book_Managment.Models.Books;

namespace Student_Managment.Models
{
    internal class Student
    {
        public static readonly HashSet<char> grades = new HashSet<char>() { 'A', 'B', 'C', 'D', 'F' }; // set of chars which is used to prevent invalid grades via .contains().
        public HashSet<Book> BorrowedBooks = []; // storing the set of books that this student has borrowed from the library.
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int RollNum { get; set; }
        private static int _Count = 0;
        [HideInTable]
        public static int Count
        {
            get
            {
                return _Count;
            }

            set
            {
                _Count = Math.Max(_Count, value);
            }
        }
        private char _grade;
        public char Grade
        {
            get
            {
                return _grade;
            }

            set
            {
                if (IsValidGrade(value))
                {
                    _grade = value;
                }
                else
                {
                    throw new GradeInvalidException();
                }
            }

        }


        public static bool IsValidGrade(char grade) => grades.Contains(grade); // preventing invalid grade arguments.


        public Student(string name, int rollNum, char grade)
        {
            Count++;
            Id = Count;
            Name = name;
            RollNum = rollNum;
            Grade = grade;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Roll Number: {RollNum}, Grade: {Grade}.\n";
        }
    }
}
