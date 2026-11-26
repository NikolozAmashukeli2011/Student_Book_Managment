using Student_Book_Managment.Helpers.UI.Printers;
using System.Globalization;

namespace Student_Book_Managment.Helpers.Prompters
{
    static class Prompter // a static helper class, whos sole purpose is to get user input, validate it, and run a loop while until the desired type is inputted, which is then returned.
    {
        public static T PromptGeneric<T>(string startingTxt, string errTxt) where T : IParsable<T> // IParsable<T> means this type can be created from a string value.
        {
            T result;

            Console.WriteLine(startingTxt);
            string? input = Console.ReadLine();

            while (!T.TryParse(input, CultureInfo.InvariantCulture, out result!))
            {
                MarkupPrinter.PrintError(errTxt);
                input = Console.ReadLine();
            }

            return result;
        }

        public static string PromptString(string startingTxt, string typeErrTxt, string retryTxt = "Input mustn't be empty, Try again: ")
        {
            string result = PromptGeneric<string>(startingTxt, typeErrTxt);

            while (string.IsNullOrWhiteSpace(result))
            {
                result = PromptGeneric<string>(retryTxt, typeErrTxt);
            }

            return result;
        }

        public static bool PromptChoice(string txt = "Input your choice (Yes or No): ", string errTxt = "Input must be either Yes, or No, try again: ",
           string retryTxt = "Input must be either yes, or no, try again: ", string choice1 = "Yes", string choice2 = "No")
        {

            string choice = PromptString(txt, errTxt).Trim();

            bool ChoiceIsFirst() => string.Equals(choice, choice1, StringComparison.OrdinalIgnoreCase);

            bool ChoiceIsSecond() => string.Equals(choice, choice2, StringComparison.OrdinalIgnoreCase);

            while (!ChoiceIsFirst() && !ChoiceIsSecond())
            {
                choice = PromptGeneric<string>(retryTxt, errTxt).Trim();
            }

            return ChoiceIsFirst();
        }

        public static T PromptEnum<T>(string txt, string retryTxt, string errTxt, bool ignoreCase = true) where T : struct, Enum // struct, Enum filter was neccessary,
                                                                                                                                 // since Enum on its own allows all types that are derived from the built in enum class, including system.Enum itself, which is wrong.
                                                                                                                                 // struct filter allows only value types, that include int, datetime and others, not only enum.
                                                                                                                                 // both combined: only value types, that are derived from system.Enum.

        {
            T result;

            string input = PromptGeneric<string>(txt, errTxt).Trim();

            while (!Enum.TryParse(input, ignoreCase, out result) || int.TryParse(input, out _)) // int.TryParse check is neccessary since enum members are numbers underneath (starting from 0),
                                                                                                // and i want only text input to be valid from the user.
            {
                input = PromptGeneric<string>(retryTxt, errTxt).Trim();
            }

            return result;
        }

    }
}
