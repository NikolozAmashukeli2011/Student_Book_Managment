using System.Globalization;

namespace Student_Managment.Helpers
{
    static class Prompter
    {
        public static T PromptGeneric<T>(string startingTxt, string errTxt) where T : IParsable<T>
        {
            T result;

            Console.WriteLine(startingTxt);
            string? input = Console.ReadLine();

            while (!T.TryParse(input, CultureInfo.InvariantCulture, out result!))
            {
                Console.WriteLine(errTxt);
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
           string retryTxt = "Input must be either yes, or no, try again: ")
        {

            string choice =  PromptString(txt, errTxt).Trim();

            bool ChoiceIsYes() => string.Equals(choice, "Yes", StringComparison.OrdinalIgnoreCase);

            bool ChoiceIsNo() => string.Equals(choice, "No", StringComparison.OrdinalIgnoreCase);

            while (!ChoiceIsYes() && !ChoiceIsNo())
            {
                choice = PromptGeneric<string>(retryTxt, errTxt).Trim();
            }

            return ChoiceIsYes();
        }
    }
}
