using System;
using System.Collections.Generic;
using System.Text;

namespace Student_Book_Managment.Enums
{
    public enum MenuAction
    {
        // Add
        AddStudent,
        AddBook,

        // Update
        UpdateStudentGrade,

        // Library (student)
        BorrowBook,
        ReturnBook,
        DisplayBorrowedBooks,

        // Display
        DisplayAllStudents,
        DisplayStudentsByGrade,
        DisplayAllBooks,
        DisplayBooksByYear,

        // Find
        FindBookById,
        FindBookByTitle,
        FindStudentById,
        FindStudentsByRollNumber,

        // Remove
        RemoveStudentById,
        RemoveBookById,
        RemoveAllStudents,
        RemoveAllBooks,

        // Exit
        Exit
    }
}
