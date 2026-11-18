namespace Student_Book_Managment.Models.Books
{
    internal class BookStorageData // a wrapper class, which is a blueprint of how book data should be saved into the json files.
    {
        public int IdCount { get; set; } // storing id count so my program doesn't forget the count after a session,
                                         // which then may lead to duplicate id issue, resulting in an exception.
        public IEnumerable<Book> Books { get; set; } = new List<Book>();
        public BookStorageData() { }
        public BookStorageData(int idCount, IEnumerable<Book> books)
        {
            IdCount = idCount;
            Books = books;
        }

    }
}
