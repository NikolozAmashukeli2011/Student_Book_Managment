using Student_Book_Managment.Models.Books;
using Student_Book_Managment.Services.Managers;

namespace Student_Book_Managment.Helpers.Prompters
{
    internal class BookPrompter // Pretty much the same as Prompter class, just dedicated to Books.
    {
        public static int PromptId(string txt = "Input book's Id: ", string errTxt = "Input must be an integer, try again: ") => Prompter.PromptGeneric<int>(txt, errTxt);

        public static string PromptTitle(string txt = "Input book's title: ", string errTxt = "input must be a text, try again: ",
        string retryTxt = "Input mustn't be an empty text, and should contain atleast 1 letter, try again: ")
        {
            string title = Prompter.PromptString(txt, errTxt, retryTxt);

            while (title.All(char.IsDigit))
            {
                title = Prompter.PromptString(retryTxt, errTxt, retryTxt);
            }

            return title.Trim();
        }

        public static string PromptAuthor(string txt = "Input new book's author: ", string errTxt = "input must be a text, try again: ",
                                          string retryTxt = "Input mustn't be an empty text, and should contain atleast 1 letter, try again: ")
        {
            string author = Prompter.PromptString(txt, errTxt, retryTxt);

            while (author.All(char.IsDigit))
            {
                author = Prompter.PromptString(retryTxt, errTxt, retryTxt);
            }

            return author.Trim();
        }

        public static int PromptYear(string txt = "Input new book's year of publish: ", string errTxt = "Input must be an integer, try again: ",
                                    string retryTtxt = "New book's year of publish must be above 0, and mustn't be above 2026, try again: ")
        {
            int yearOfPublish = Prompter.PromptGeneric<int>(txt, errTxt);

            while (yearOfPublish < 1 || yearOfPublish > 2026)
            {
                yearOfPublish = Prompter.PromptGeneric<int>(retryTtxt, errTxt);
            }

            return yearOfPublish;
        }

        public static Book PromptBook()
        {
            string title = PromptTitle();
            string author = PromptAuthor();
            int yearOfPublish = PromptYear();

            return new Book(title, author, yearOfPublish);
        }
    }
}
