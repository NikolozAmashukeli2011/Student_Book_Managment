using Student_Book_Managment.Exceptions;
using Student_Book_Managment.Models.Books;
using Student_Managment.Models;

namespace Student_Book_Managment.Services.Managers
{
    internal static class LibraryManager // Separate Manager for the library, containing Dictionary with key of book id,
                                         // and value of the student id who has borrowed it.
    {
        private static readonly Dictionary<int, int> BookIdToBorrowerId = new Dictionary<int, int>();

        public static void BorrowBook(Student student, Book book)
        {
            if (book.BorrowedByStudentId == null) // if no one is borrowing this book currently, we set the property to student.id.
            {
                book.BorrowedByStudentId = student.Id;

                if (BookIdToBorrowerId.ContainsKey(book.Id)) // if the key of book.id already exists, we modify its value to student.id.
                {
                    BookIdToBorrowerId[book.Id] = student.Id;
                }
                else
                {
                    BookIdToBorrowerId.Add(book.Id, student.Id); // otherwise, we create a brand new kvp.
                }

                student.BorrowedBooks.Add(book); // adding book object to borrowed books property of the student.
            }
            else if (book.BorrowedByStudentId == student.Id) // if the student who is trying to borrow this book is already borrowing this book,
                                                             // or book's borrowedByStudentId isnt null, meaning it is borrowed by someone else,
                                                             // we throw an exception.
            {
                throw new BookBorrowedException("You have already borrowed this book.");
            }
            else
            {
                throw new BookBorrowedException($"Book is already borrowed by student with id: {student.Id}.");
            }
        }

        public static bool ReturnBook(Student student, int bookId)
        {
            BookManager.GetBookById(bookId, out Book? book);

            if (student.BorrowedBooks.Remove(book!)) // .Remove() on collections returns true if element is successfully found and removed,
                                                     // so here, if student.BorrowedBooks.Remove() was successfull, we set books borrowedByStudentId to null.
                                                     // BookIdToBorrowerId dictionary gets kvp with key of bookId removed, and we return true. otherwise false.
            {
                book!.BorrowedByStudentId = null;
                BookIdToBorrowerId.Remove(bookId);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void DisplayBorrowedBooks(Student student)
        {
            if (student.BorrowedBooks.Count == 0)
            {
                Console.WriteLine("Student hasn't borrowed any books!\n");
            }
            else
            {
                foreach (Book book in student.BorrowedBooks)
                {
                    Console.WriteLine(book);
                }
            }
        }

    }
}
