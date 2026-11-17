using Student_Book_Managment.Exceptions;

namespace Student_Book_Managment.Models.Books
{
    internal class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int? BorrowedByStudentId { get; set; } = null;
        private static int _Count = 0;
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
        private int _YearOfPublish;
        public int YearOfPublish
        {
            get
            {
                return _YearOfPublish;
            }

            set
            {
                if (value < 1 || value > 2026)
                {
                    throw new YearInvalidException();
                }
                else
                {
                    _YearOfPublish = value;
                }
            }
        }

        public Book()
        {

        }

        public Book(string title, string author, int yearOfPublish)
        {
            Count++;
            Id = Count;
            Title = title;
            Author = author;
            YearOfPublish = yearOfPublish;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Title: {Title}, Author: {Author}, Year Of Publish: {YearOfPublish}.\n";
        }
    }
}
