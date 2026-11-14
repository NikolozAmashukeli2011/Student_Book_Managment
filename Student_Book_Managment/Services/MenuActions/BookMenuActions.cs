using Student_Book_Managment.Exceptions;
using Student_Book_Managment.Helpers;
using Student_Book_Managment.Models.Books;
using Student_Book_Managment.Services.Managers;
using Student_Book_Managment.Services.StudentsService;
using Student_Managment.Helpers;
using Student_Managment.Models;

namespace Student_Book_Managment.Services.MenuActions
{
    static class BookMenuActions
    {

        public static void AddBook()
        {
            Book book = BookPrompter.PromptBook();

            bool success = BookManager.AddBook(book);

            if (success)
            {
                Console.WriteLine("Book added successfully!\n");
                Console.WriteLine($"{book}\n");
            }
            else
            {
                Console.WriteLine("Adding the book failed!\n");
            }
        }

        public static void BorrowBook()
        {
            int studentId = StudentPrompter.PromptId();

            if (!StudentManager.IdIsValid(studentId))
            {
                Console.WriteLine($"Student with id: {studentId} Couldn't be found!");
                return;
            }

            StudentManager.FindStudentById(studentId, out Student? student);

            int bookId = BookPrompter.PromptId();

            if (!BookManager.IdIsValid(bookId))
            {
                Console.WriteLine($"Book with id: {bookId} doesn't exist!");
                return;
            }

            BookManager.GetBookById(bookId, out Book? book);

            try
            {
                LoanManager.BorrowBook(student!, book!);

                Console.WriteLine($"{book!.Title} borrowed successfully!");
            }
            catch (BookBorrowedException ex)
            {
                Console.WriteLine($"{ex.Message}. Thrown at: {ex.ThrownAt}");
            }
        }

        public static void ReturnBook()
        {
            int studentId = StudentPrompter.PromptId();

            if (!StudentManager.IdIsValid(studentId))
            {
                Console.WriteLine($"\nStudent with id: {studentId} Couldn't be found!");
                return;
            }

            StudentManager.FindStudentById(studentId, out Student? student);

            int bookId = BookPrompter.PromptId();

            if (!BookManager.IdIsValid(bookId))
            {
                Console.WriteLine($"\nBook with id: {bookId} Couldn't be found!");
                return;
            }

            if (LoanManager.ReturnBook(student!, bookId))
            {
                Console.WriteLine("\nBook returned successfully!");
            }
            else
            {
                Console.WriteLine("\nYou haven't even borrowed this book!");
            }
        }

        public static void FindBookByTitle()
        {
            string title = BookPrompter.PromptTitle();

            if (BookManager.GetBookByTitle(title, out Book? foundBook))
            {
                Console.WriteLine("\nBook found.\n");
                Console.WriteLine($"{foundBook?.ToString()}\n");
            }
            else
            {
                Console.WriteLine($"Book with title: {title} couldn't be found.\n");
            }
        }

        public static void FindBookById()
        {
            int id = BookPrompter.PromptId();

            if (BookManager.GetBookById(id, out Book? foundBook))
            {
                Console.WriteLine("\nBook found.\n");
                Console.WriteLine($"{foundBook?.ToString()}\n");
            }
            else
            {
                Console.WriteLine($"\nBook with id: {id} couldn't be found.");
            }
        }

        public static void RemoveBookById()
        {
            int id = BookPrompter.PromptId();

            if (BookManager.GetBookById(id, out Book? foundBook))
            {
                BookManager.RemoveBook(foundBook!);

                Console.WriteLine("Book removed successfully!\n");
                Console.WriteLine($"{foundBook!}\n");
            }
            else
            {
                Console.WriteLine($"\nBook with id: {id} couldn't be found.");
            }

        }

        public static void RemoveAllBooks()
        {

            string choice = Prompter.PromptGeneric<string>("Are you sure that you want to remove all books? (Yes or No):",
                                                            "Input must be a text, try again: ").Trim();

            bool sure = string.Equals(choice, "Yes", StringComparison.OrdinalIgnoreCase);
            bool unSure = string.Equals(choice, "No", StringComparison.OrdinalIgnoreCase);

            while (!sure && !unSure)
            {
                choice = Prompter.PromptGeneric<string>("Input must be either yes, or no, try again: ",
                                                        "Input must be a text, try again: ").Trim();

                sure = string.Equals(choice, "Yes", StringComparison.OrdinalIgnoreCase);
                unSure = string.Equals(choice, "No", StringComparison.OrdinalIgnoreCase);
            }

            if (sure)
            {
                if (BookManager.RemoveAllBooks())
                {
                    Console.WriteLine("\nBooks removed Successfully!\n");
                }
                else
                {
                    Console.WriteLine("\nThere are no books!");
                }
            }
        }

    }
}
