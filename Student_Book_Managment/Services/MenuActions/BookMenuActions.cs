using Spectre.Console;
using Student_Book_Managment.Exceptions;
using Student_Book_Managment.Helpers.Printers;
using Student_Book_Managment.Helpers.Printers.TableStyles;
using Student_Book_Managment.Helpers.Prompters;
using Student_Book_Managment.Helpers.UI.Printers;
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
                MarkupPrinter.PrintSuccess("Book added successfully!\n");
                TablePrinter.PrintObj(book, MethodStyles.AddStyles);
            }
            else
            {
                MarkupPrinter.PrintError("Adding the book failed!\n");
            }
        }

        public static void BorrowBook()
        {
            int bookId = BookPrompter.PromptId();

            if (!BookManager.IdIsValid(bookId))
            {
                MarkupPrinter.PrintError($"Book with id: {bookId} doesn't exist!\n");
                return;
            }

            BookManager.GetBookById(bookId, out Book? book);

            try
            {
                LibraryManager.BorrowBook(Session.CurrentStudent!, book!);

                MarkupPrinter.PrintSuccess($"{Session.CurrentStudent!.Name} borrowed {book!.Title} successfully!");
            }
            catch (BookBorrowedException ex)
            {
                MarkupPrinter.PrintError($"{ex.Message}. Thrown at: {ex.ThrownAt}");
            }
        }

        public static void ReturnBook()
        {
            int bookId = BookPrompter.PromptId();

            if (!BookManager.IdIsValid(bookId))
            {
                MarkupPrinter.PrintError($"\nBook with id: {bookId} Couldn't be found!");
                return;
            }

            if (LibraryManager.ReturnBook(Session.CurrentStudent!, bookId))
            {
                MarkupPrinter.PrintSuccess("\nBook returned successfully!");
            }
            else
            {
                MarkupPrinter.PrintError("\nYou haven't even borrowed this book!");
            }
        }

        public static void DisplayBorrowedBooks() => LibraryManager.DisplayBorrowedBooks(Session.CurrentStudent!);

        public static void FindBookByTitle()
        {
            string title = BookPrompter.PromptTitle();

            if (BookManager.GetBookByTitle(title, out Book? foundBook))
            {
                MarkupPrinter.PrintSuccess("\nBook found!\n");
                TablePrinter.PrintObj(foundBook!, MethodStyles.FindStyles);
            }
            else
            {
                MarkupPrinter.PrintError($"Book with title: {title} couldn't be found.\n");
            }
        }

        public static void FindBookById()
        {
            int id = BookPrompter.PromptId();

            if (BookManager.GetBookById(id, out Book? foundBook))
            {
                MarkupPrinter.PrintSuccess("\nBook found!\n");
                TablePrinter.PrintObj(foundBook!, MethodStyles.FindStyles);
            }
            else
            {
                MarkupPrinter.PrintError($"\nBook with id: {id} couldn't be found.");
            }
        }

        public static void RemoveBookById()
        {
            int id = BookPrompter.PromptId();

            if (BookManager.GetBookById(id, out Book? foundBook))
            {
                BookManager.RemoveBook(foundBook!);

                MarkupPrinter.PrintSuccess("\nBook removed successfully!");
                TablePrinter.PrintObj(foundBook!, MethodStyles.RemoveStyles);
            }
            else
            {
                MarkupPrinter.PrintError($"\nBook with id: {id} couldn't be found.");
            }

        }

        public static void RemoveAllBooks()
        {
            bool sure = Prompter.PromptChoice();

            if (sure)
            {
                if (BookManager.RemoveAllBooks())
                {
                    MarkupPrinter.PrintSuccess("\nBooks removed successfully!");
                }
                else
                {
                    MarkupPrinter.PrintError("\nThere are no Books!");
                }
            }
        }

    }
}
