using Student_Book_Managment.Exceptions;
using Student_Book_Managment.Models.Books;
using Student_Managment.Models;

namespace Student_Book_Managment.Services.Managers
{
    internal static class LibraryManager
    {
        public static Dictionary<Student, List<Book>> StudentToBorrowedBooksMap = new Dictionary<Student, List<Book>>();
        public static Dictionary<int, int> BookIdToBorrowerId = new Dictionary<int, int>();

        public static void BorrowBook(Student student, Book book)
        {
            if (book.BorrowedByStudentId == null)
            {
                book.BorrowedByStudentId = student.Id;

                if (BookIdToBorrowerId.ContainsKey(student.Id))
                {
                    BookIdToBorrowerId[book.Id] = student.Id;
                }
                else
                {
                    BookIdToBorrowerId.Add(book.Id, student.Id);
                }

                student.BorrowedBooks.Add(book);
            }
            else if (book.BorrowedByStudentId == student.Id)
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

            if (student.BorrowedBooks.Remove(book!))
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
