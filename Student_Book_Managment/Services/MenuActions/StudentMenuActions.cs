using Student_Book_Managment.Helpers.Prompters;
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
                Console.WriteLine("Student added successfully!\n");
                Console.WriteLine(studentToAdd);

            }
            else
            {
                Console.WriteLine("adding the Student failed!\n");
            }
        }

        public static void ModifyGrade()
        {
            int id = StudentPrompter.PromptId();

            StudentManager.FindStudentById(id, out Student? foundStudent);

            if (foundStudent == null)
            {
                Console.WriteLine($"Student with id: {id} couldn't be found!\n");
            }
            else
            {
                char grade = StudentPrompter.PromptGrade();

                StudentManager.ModifyGrade(foundStudent!, grade);

                Console.WriteLine("\nStudent's grade modified successfully!");
            }
        }

        public static void RemoveStudentByIdAction()
        {
            int id = StudentPrompter.PromptId();

            bool foundSuccessfully = StudentManager.FindStudentById(id, out Student? student);

            if (foundSuccessfully)
            {
                StudentManager.RemoveStudent(student!);
                Console.WriteLine("Student removed successfully!\n");
                Console.WriteLine(student!);
            }
            else
            {
                Console.WriteLine($"\nStudent with id: {id} couldn't be found.\n");
            }
        }

        public static void FindStudentsByRollNum()
        {
            int rollNum = StudentPrompter.PromptRollNum();

            bool success = StudentManager.FindStudentsByRollNum(rollNum, out List<Student>? students);

            if (success)
            {
                {
                    Console.WriteLine($"\n[ROLL {rollNum}]\n");

                    foreach (Student student in students!)
                    {
                        Console.WriteLine(student);
                    }
                }

            }
            else
            {
                Console.WriteLine($"Students in roll number: {rollNum} couldn't be found.\n");
            }
        }
        public static void FindStudentByIdAction()
        {
            int id = StudentPrompter.PromptId();

            bool foundSuccessfully = StudentManager.FindStudentById(id, out Student? student);

            if (foundSuccessfully)
            {
                Console.WriteLine("Student found successfully!\n");
                Console.WriteLine(student!);
            }
            else
            {
                Console.WriteLine($"Student with id: {id} couldn't be found.\n");
            }
        }

        public static void RemoveAllStudents()
        {

            bool sure = Prompter.PromptChoice();

            if (sure)
            {
                if (StudentManager.RemoveAllStudents())
                {
                    Console.WriteLine("\nStudents removed successfully!");
                }
                else
                {
                    Console.WriteLine("\nThere are no students!");
                }
            }
        }

    }
}
