using Student_Book_Managment.DataHandlers;
using Student_Book_Managment.Models.Books;

namespace Student_Book_Managment.Services.Managers
{
    static class BookManager // Manager class, which contains multiple data structures for fast lookups, and methods that operate on books data.
    {
        public static HashSet<Book> SetOfBooks = new HashSet<Book>();
        public static Dictionary<string, HashSet<Book>> AuthorToBooksSetMap = new Dictionary<string, HashSet<Book>>(StringComparer.OrdinalIgnoreCase);
        public static Dictionary<string, Book> TitleToBookMap = new Dictionary<string, Book>(StringComparer.OrdinalIgnoreCase);
        public static Dictionary<int, Book> IdToBookMap = new Dictionary<int, Book>();
        public static SortedList<int, HashSet<Book>> YearToBooksSortedList = new SortedList<int, HashSet<Book>>();
        private static readonly string FilePath = @"C:\Users\user\Desktop\Backend\Projects\Student&BookManagment\Student_Book_Managment\JsonFolders\Books.json";

        public static async Task InitializeAsync()
        {
            BookStorageData data = await JsonFIleStorage.LoadAsync<BookStorageData>(FilePath);

            Book.Count = data.IdCount;

            foreach (Book b in data.Books) // looping over BookStorageData.Books IEnumerable and refilling collections.
            {
                AddToAll(b);
            }
        }

        private static async void SaveAllAsync() => await JsonFIleStorage.SaveAsync(new BookStorageData(Book.Count, SetOfBooks), FilePath);
            
        private static void AddToAll(Book b)
        {
            if (!AuthorToBooksSetMap.TryGetValue(b.Author, out HashSet<Book>? booksSet))
            {
                booksSet = new HashSet<Book>();
                AuthorToBooksSetMap[b.Author] = booksSet;
            }

            if (!YearToBooksSortedList.TryGetValue(b.YearOfPublish, out HashSet<Book>? booksByYear))
            {
                booksByYear = new HashSet<Book>();
                YearToBooksSortedList[b.YearOfPublish] = booksByYear;
            }

            booksSet.Add(b);
            booksByYear.Add(b);
            TitleToBookMap.Add(b.Title, b);
            SetOfBooks.Add(b);
            IdToBookMap.Add(b.Id, b);
        }

        private static bool BookExists(Book b) => IdToBookMap.ContainsKey(b.Id);

        public static bool AddBook(Book book)
        {
            if (BookExists(book))
            {
                return false;
            }
            else
            {
                AddToAll(book);
                SaveAllAsync();
                return true;
            }
        }

        public static bool IdIsValid(int id) => IdToBookMap.ContainsKey(id);

        public static void DisplayAllBooks()
        {
            if (SetOfBooks.Count == 0)
            {
                Console.WriteLine("\nThere are no books.");
            }
            else
            {
                Console.WriteLine("\n[SET OF BOOKS]\n");

                foreach (Book book in SetOfBooks)
                {
                    Console.WriteLine(book);
                }
            }

        }

        public static void DisplayBooksByYear()
        {
            if (YearToBooksSortedList.Count == 0)
            {
                Console.WriteLine("\nThere are no books.");
            }
            else
            {
                Console.WriteLine("\n[BOOKS BY YEAR]\n");

                foreach ((int year, HashSet<Book> books) in YearToBooksSortedList)
                {
                    Console.WriteLine($"[{year}]\n");

                    foreach (Book book in books)
                    {
                        Console.WriteLine(book);
                    }
                }
            }
        }

        public static bool GetBookByTitle(string title, out Book? foundBook)
        {
            TitleToBookMap.TryGetValue(title, out foundBook);
            return foundBook != null;
        }

        public static bool GetBookById(int id, out Book? foundBook)
        {
            IdToBookMap.TryGetValue(id, out foundBook);
            return foundBook != null;
        }

        public static bool RemoveBook(Book book)
        {
            if (BookExists(book))
            {
                AuthorToBooksSetMap.TryGetValue(book.Author, out HashSet<Book>? authorSet);

                if (authorSet?.Count <= 1)
                {
                    AuthorToBooksSetMap.Remove(book.Author);
                }
                else
                {
                    authorSet?.Remove(book);
                }

                YearToBooksSortedList.TryGetValue(book.YearOfPublish, out HashSet<Book>? yearSet);

                if (yearSet?.Count <= 1)
                {
                    YearToBooksSortedList.Remove(book.YearOfPublish);
                }
                else
                {
                    yearSet?.Remove(book);
                }

                SetOfBooks.Remove(book);
                TitleToBookMap.Remove(book.Title);
                IdToBookMap.Remove(book.Id);

                SaveAllAsync();

                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool RemoveAllBooks()
        {
            if (SetOfBooks.Count > 0)
            {
                SetOfBooks.Clear();
                TitleToBookMap.Clear();
                IdToBookMap.Clear();
                AuthorToBooksSetMap.Clear();
                YearToBooksSortedList.Clear();
                SaveAllAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
