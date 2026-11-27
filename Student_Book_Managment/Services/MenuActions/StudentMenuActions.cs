using Student_Book_Managment.Helpers.Printers;
using Student_Book_Managment.Helpers.Printers.TableStyles;
using Student_Book_Managment.Helpers.Prompters;
using Student_Book_Managment.Helpers.UI.Printers;
using Student_Book_Managment.Services.StudentsService;
using Student_Managment.Models;

namespace Student_Book_Managment.Services.MenuActions
{
    static class StudentMenuActions
    {
        public static void AddStudentAction()
        {
            Student studentToAdd = StudentPrompter.PromptStudent();

            bool success = StudentManager.AddStudent(studentToAdd);

            if (success)
            {
                MarkupPrinter.PrintSuccess("\nStudent added successfully!");
                TablePrinter.PrintObj(studentToAdd, MethodStyles.AddStyles);
            }
            else
            {
                MarkupPrinter.PrintError("\nadding the Student failed!");
            }
        }

        public static void ModifyGrade()
        {
            if (StudentManager.IsEmpty())
            {
                MarkupPrinter.PrintError("\nThere are no students!");
                return;
            }

            int id = StudentPrompter.PromptId();

            StudentManager.FindStudentById(id, out Student? foundStudent);

            if (foundStudent == null)
            {
                MarkupPrinter.PrintError($"\nStudent with id: {id} couldn't be found!");
            }
            else
            {
                char grade = StudentPrompter.PromptGrade();
                char pastGrade = foundStudent.Grade;

                if (StudentManager.ModifyGrade(foundStudent!, grade))
                {
                    MarkupPrinter.PrintSuccess($"\n{foundStudent.Name}'s grade modified successfully! ({pastGrade} -> {grade}).");
                }
                else
                {
                    MarkupPrinter.PrintError($"\n{foundStudent.Name}'s grade is already {grade}.");
                }
            }
        }

        public static void RemoveStudentByIdAction()
        {
            int id = StudentPrompter.PromptId();

            bool foundSuccessfully = StudentManager.FindStudentById(id, out Student? student);

            if (foundSuccessfully)
            {
                StudentManager.RemoveStudent(student!);
                MarkupPrinter.PrintSuccess("\nStudent removed successfully!");
                TablePrinter.PrintObj(student!, MethodStyles.RemoveStyles);
            }
            else
            {
                MarkupPrinter.PrintError($"\nStudent with id: {id} couldn't be found.");
            }
        }

        public static void FindStudentsByRollNum()
        {
            int rollNum = StudentPrompter.PromptRollNum();

            bool success = StudentManager.FindStudentsByRollNum(rollNum, out List<Student>? students);

            if (success)
            {
                {
                    MarkupPrinter.PrintColoredHeader($"ROLL [{rollNum}]", MethodStyles.FindStyles);

                    foreach (Student student in students!)
                    {
                        TablePrinter.PrintObj(student, MethodStyles.FindStyles);
                    }
                }

            }
            else
            {
                MarkupPrinter.PrintError($"\nStudents in roll number: {rollNum} couldn't be found.");
            }
        }
        public static void FindStudentByIdAction()
        {
            int id = StudentPrompter.PromptId();

            bool foundSuccessfully = StudentManager.FindStudentById(id, out Student? student);

            if (foundSuccessfully)
            {
                MarkupPrinter.PrintSuccess("\nStudent found successfully!");
                TablePrinter.PrintObj(student!, MethodStyles.FindStyles);
            }
            else
            {
                MarkupPrinter.PrintError($"\nStudent with id: {id} couldn't be found.");
            }
        }

        public static void RemoveAllStudents()
        {

            bool sure = Prompter.PromptChoice();

            if (sure)
            {
                if (StudentManager.RemoveAllStudents())
                {
                    MarkupPrinter.PrintSuccess("\nStudents removed successfully!");
                }
                else
                {
                    MarkupPrinter.PrintError("\nThere are no students!");
                }
            }
        }

    }
}
