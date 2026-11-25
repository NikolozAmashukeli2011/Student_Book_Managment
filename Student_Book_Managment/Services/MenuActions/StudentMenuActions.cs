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
                MarkupPrinter.PrintSuccess("Student added successfully!\n");
                TablePrinter.PrintObj(studentToAdd, MethodStyles.AddStyles);
            }
            else
            {
                MarkupPrinter.PrintError("adding the Student failed!\n");
            }
        }

        public static void ModifyGrade()
        {
            int id = StudentPrompter.PromptId();

            StudentManager.FindStudentById(id, out Student? foundStudent);

            if (foundStudent == null)
            {
                MarkupPrinter.PrintError($"Student with id: {id} couldn't be found!\n");
            }
            else
            {
                char grade = StudentPrompter.PromptGrade();

                StudentManager.ModifyGrade(foundStudent!, grade);

                MarkupPrinter.PrintSuccess("\nStudent's grade modified successfully!");
            }
        }

        public static void RemoveStudentByIdAction()
        {
            int id = StudentPrompter.PromptId();

            bool foundSuccessfully = StudentManager.FindStudentById(id, out Student? student);

            if (foundSuccessfully)
            {
                StudentManager.RemoveStudent(student!);
                MarkupPrinter.PrintSuccess("Student removed successfully!\n");
                TablePrinter.PrintObj(student!, MethodStyles.RemoveStyles);
            }
            else
            {
                MarkupPrinter.PrintError($"\nStudent with id: {id} couldn't be found.\n");
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
                MarkupPrinter.PrintError($"Students in roll number: {rollNum} couldn't be found.\n");
            }
        }
        public static void FindStudentByIdAction()
        {
            int id = StudentPrompter.PromptId();

            bool foundSuccessfully = StudentManager.FindStudentById(id, out Student? student);

            if (foundSuccessfully)
            {
                MarkupPrinter.PrintSuccess("Student found successfully!\n");
                TablePrinter.PrintObj(student!, MethodStyles.FindStyles);
            }
            else
            {
                MarkupPrinter.PrintError($"Student with id: {id} couldn't be found.\n");
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
