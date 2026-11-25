using Student_Managment.Models;

namespace Student_Book_Managment.Helpers.Prompters
{
    internal class StudentPrompter
    {

        public static int PromptId(string txt = "Input Student's Id: ", string errTxt = "Input must be an integer, try again: ") => Prompter.PromptGeneric<int>(txt, errTxt);

        public static string PromptName(string txt = "Input student's name: ", string errTxt = "input must be a text, try again: ",
        string retryTxt = "Input mustn't be an empty text, and shouldn't contain a number, try again: ")
        {
            string name = Prompter.PromptString(txt, errTxt, retryTxt);

            while (name.Any(char.IsDigit)) // checking if atleast one char is a digit, if so, we make user input again.
            {
                name = Prompter.PromptString(txt, retryTxt, errTxt);
            }

            return name.Trim();
        }

        public static int PromptRollNum(string txt = "Input student's roll number: ", string errTxt = "input must be an integer, try again: ") => Prompter.PromptGeneric<int>(txt, errTxt);

        public static char PromptGrade()
        {
            char grade = char.ToUpper(Prompter.PromptGeneric<char>("Input student's grade: ", "Grade must be a char, try again: "));

            while (!Student.IsValidGrade(grade))
            {
                grade = char.ToUpper(Prompter.PromptGeneric<char>("Input must be a char (A-F, excluding E), try again: ", "Grade must be a char, try again: "));
            }

            return grade;
        }

        public static Student PromptStudent()
        {
            string name = PromptName();
            int rollNum = PromptRollNum();
            char grade = PromptGrade();

            return new Student(name, rollNum, grade);
        }
    }
}
