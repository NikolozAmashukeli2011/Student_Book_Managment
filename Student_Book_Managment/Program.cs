using Student_Book_Managment.Services.BooksService;
using Student_Book_Managment.Services.StudentsService;
using Student_Managment.Helpers;

int choice;
int methodAmout = 17;

do
{
    Console.WriteLine(
        "\n[ADD]\n" +
        "1. Add a student.\n" +
        "2. Add a book.\n" +
        "\n[UPDATE]\n" +
        "3. Update student's grade.\n" +
        "\n[DISPLAY]\n" +
        "4. Display all Students.\n" +
        "5. Display students by grade.\n" +
        "6. Display all books.\n" +
        "7. Display books by year.\n" +
        "\n[FIND]\n" +
        "8. Find Student by id.\n" +
        "9. Find Students by roll number.\n" +
        "10. Find book by id.\n" +
        "11. Find book by title\n" +
        "\n[REMOVE]\n" +
        "12. Remove student by id.\n" +
        "13. Remove book by id.\n" +
        "14. Remove all students.\n" +
        "15. Remove all books.\n" +
        "16. Remove all Data.\n" +
        "\n[EXIT]\n" +
        "17. Exit.\n"
        );

    string intErrTxt = "Input must be an integer, try again: ";

    choice = Prompter.PromptGeneric<int>($"Input a number (1-{methodAmout}): ", intErrTxt);

    while (choice < 1 || choice > methodAmout)
    {
        choice = Prompter.PromptGeneric<int>($"Input must be a number 1-{methodAmout}. try again: ", intErrTxt);
    }

    switch (choice)
    {
        case 1: StudentMenuActions.AddStudentAction(); break;
        case 2: BookMenuActions.AddBook(); break;
        case 3: break;
        case 4: StudentManager.DisplayAllStudents(); break;
        case 5: StudentManager.DisplayStudentsByGrade(); break;
        case 6: BookManager.DisplayAllBooks(); break;
        case 7: BookManager.DisplayBooksByYear(); break;
        case 8: StudentMenuActions.FindStudentByIdAction(); break;
        case 9: StudentMenuActions.FindStudentsByRollNum(); break;
        case 10: BookMenuActions.FindBookById(); break;
        case 11: BookMenuActions.FindBookByTitle(); break;
        case 12: StudentMenuActions.RemoveStudentByIdAction(); break;
        case 13: BookMenuActions.RemoveBookById(); break;
        case 14: StudentMenuActions.RemoveAllStudents(); break;
        case 15: BookMenuActions.RemoveAllBooks(); break;
        case 16: break;
        case 17: break;
    }


} while (choice != methodAmout);

