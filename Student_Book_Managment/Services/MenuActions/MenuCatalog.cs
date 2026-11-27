using Student_Book_Managment.Enums;
using Student_Book_Managment.Helpers.UI.Styles;
using Student_Book_Managment.Services.Managers;
using Student_Book_Managment.Services.StudentsService;

namespace Student_Book_Managment.Services.MenuActions
{
    public static class MenuCatalog
    {
        public static MenuAction[] StudentActions = // array holding Actions that student role can run.
        {
            // Library
            MenuAction.BorrowBook,
            MenuAction.ReturnBook,

            // Display
            MenuAction.DisplayBorrowedBooks,
            MenuAction.DisplayAllBooks,
            MenuAction.DisplayBooksByYear,

            // Find
            MenuAction.FindBookById,
            MenuAction.FindBookByTitle,

            // Exit
            MenuAction.Exit
        };

        public static MenuAction[] AdminActions = // array holding Actions that admin role can run.
         {
            // Add
            MenuAction.AddStudent,
            MenuAction.AddBook,

            // Update
            MenuAction.UpdateStudentGrade,

            // Display
            MenuAction.DisplayAllStudents,
            MenuAction.DisplayStudentsByGrade,
            MenuAction.DisplayAllBooks,
            MenuAction.DisplayBooksByYear,

            // Find
            MenuAction.FindBookById,
            MenuAction.FindBookByTitle,
            MenuAction.FindStudentById,
            MenuAction.FindStudentsByRollNumber,

            // Remove
            MenuAction.RemoveStudentById,
            MenuAction.RemoveBookById,
            MenuAction.RemoveAllStudents,
            MenuAction.RemoveAllBooks,

            // Exit
            MenuAction.Exit
        };

        public static string ToDisplayText(MenuAction action) => action switch // using switch case to match Menu Action enum options to the markup text thats supposed to be displayed
                                                                               // in selectionPrompt (Ansi Console).
        {
            // Add
            MenuAction.AddStudent => "[green]Add a student[/]",
            MenuAction.AddBook => "[green]Add a book[/]",

            // Library
            MenuAction.BorrowBook => "[green]Borrow a book[/]",
            MenuAction.ReturnBook => "[green]Return a book[/]",

            // Update
            MenuAction.UpdateStudentGrade => "[yellow]Update student's grade[/]",

            // Display
            MenuAction.DisplayBorrowedBooks => "[cyan]Display borrowed books[/]",
            MenuAction.DisplayAllStudents => "[cyan]Display all students[/]",
            MenuAction.DisplayStudentsByGrade => "[cyan]Display students by grade[/]",
            MenuAction.DisplayAllBooks => "[cyan]Display all books[/]",
            MenuAction.DisplayBooksByYear => "[cyan]Display books by year[/]",

            // Find
            MenuAction.FindBookById => "[magenta]Find book by id[/]",
            MenuAction.FindBookByTitle => "[magenta]Find book by title[/]",
            MenuAction.FindStudentById => "[magenta]Find student by id[/]",
            MenuAction.FindStudentsByRollNumber => "[magenta]Find students by roll number[/]",

            // Remove
            MenuAction.RemoveStudentById => "[red]Remove student by id[/]",
            MenuAction.RemoveBookById => "[red]Remove book by id[/]",
            MenuAction.RemoveAllStudents => "[red]Remove all students[/]",
            MenuAction.RemoveAllBooks => "[red]Remove all books[/]",

            // Exit
            MenuAction.Exit => "[grey]Exit[/]",

            _ => action.ToString()
        };

        public static Dictionary<MenuAction, Action> Actions = new() // storing all action enum items as key, and actual Action (method reference) as the value.
        {
            [MenuAction.BorrowBook] = BookMenuActions.BorrowBook,
            [MenuAction.ReturnBook] = BookMenuActions.ReturnBook,
            [MenuAction.DisplayBorrowedBooks] = BookMenuActions.DisplayBorrowedBooks,
            [MenuAction.AddBook] = BookMenuActions.AddBook,
            [MenuAction.FindBookById] = BookMenuActions.FindBookById,
            [MenuAction.RemoveBookById] = BookMenuActions.RemoveBookById,
            [MenuAction.RemoveAllBooks] = BookMenuActions.RemoveAllBooks,
            [MenuAction.DisplayAllBooks] = BookManager.DisplayAllBooks,
            [MenuAction.DisplayBooksByYear] = BookManager.DisplayBooksByYear,
            [MenuAction.AddStudent] = StudentMenuActions.AddStudentAction,
            [MenuAction.UpdateStudentGrade] = StudentMenuActions.ModifyGrade,
            [MenuAction.FindStudentById] = StudentMenuActions.FindStudentByIdAction,
            [MenuAction.FindStudentsByRollNumber] = StudentMenuActions.FindStudentsByRollNum,
            [MenuAction.RemoveStudentById] = StudentMenuActions.RemoveStudentByIdAction,
            [MenuAction.RemoveAllStudents] = StudentMenuActions.RemoveAllStudents,
            [MenuAction.DisplayAllStudents] = StudentManager.DisplayAllStudents,
            [MenuAction.DisplayStudentsByGrade] = StudentManager.DisplayStudentsByGrade
        };

    }
}
