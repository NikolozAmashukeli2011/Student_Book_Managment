using Student_Book_Managment.Exceptions;
using Student_Book_Managment.Helpers.Prompters;
using Student_Book_Managment.Models.Books;
using Student_Book_Managment.Services.Managers;
using Student_Book_Managment.Services.StudentsService;
using Student_Managment.Models;

namespace Student_Book_Managment.Services.MenuActions
{
    static class BookMenuActions // MenuAction folder contains 2 static classes (Book and Student MenuActions) that have actual complete methods,
                                 // meaning: getting user input, calling manager methods, and displaying text based on the result.
    {

        public static void AddBook()
        {
            Book book = BookPrompter.PromptBook();

            bool success = BookManager.AddBook(book);

            if (success)
            {
                Console.WriteLine("Book added successfully!\n");
                Console.WriteLine(book);
            }
            else
            {
                Console.WriteLine("Adding the book failed!\n");
            }
        }

        public static void BorrowBook()
        {
            int bookId = BookPrompter.PromptId();

            if (!BookManager.IdIsValid(bookId))
            {
                Console.WriteLine($"Book with id: {bookId} doesn't exist!");
                return;
            }

            BookManager.GetBookById(bookId, out Book? book);

            try
            {
                LibraryManager.BorrowBook(Session.CurrentStudent!, book!);

                Console.WriteLine($"{Session.CurrentStudent!.Name} borrowed {book!.Title} successfully!");
            }
            catch (BookBorrowedException ex)
            {
                Console.WriteLine($"{ex.Message}. Thrown at: {ex.ThrownAt}");
            }
        }

        public static void ReturnBook()
        {
            int bookId = BookPrompter.PromptId();

            if (!BookManager.IdIsValid(bookId))
            {
                Console.WriteLine($"\nBook with id: {bookId} Couldn't be found!");
                return;
            }

            if (LibraryManager.ReturnBook(Session.CurrentStudent!, bookId))
            {
                Console.WriteLine("\nBook returned successfully!");
            }
            else
            {
                Console.WriteLine("\nYou haven't even borrowed this book!");
            }
        }

        public static void DisplayBorrowedBooks() => LibraryManager.DisplayBorrowedBooks(Session.CurrentStudent!);

        public static void FindBookByTitle()
        {
            string title = BookPrompter.PromptTitle();

            if (BookManager.GetBookByTitle(title, out Book? foundBook))
            {
                Console.WriteLine("\nBook found.\n");
                Console.WriteLine(foundBook);
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
                Console.WriteLine(foundBook);
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
                Console.WriteLine(foundBook!);
            }
            else
            {
                Console.WriteLine($"\nBook with id: {id} couldn't be found.");
            }

        }

        public static void RemoveAllBooks()
        {
            bool sure = Prompter.PromptChoice();

            if (sure)
            {
                if (BookManager.RemoveAllBooks())
                {
                    Console.WriteLine("\nBooks removed successfully!");
                }
                else
                {
                    Console.WriteLine("\nThere are no Books!");
                }
            }
        }

    }
}
