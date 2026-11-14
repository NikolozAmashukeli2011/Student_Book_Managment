using Student_Book_Managment.Services.Managers;
using Student_Book_Managment.Services.MenuActions;
using Student_Book_Managment.Services.StudentsService;
using Student_Managment.Helpers;

int choice;
int methodAmout = 18;

do
{
    Console.WriteLine(
        "\n[LIBRARY]\n"+
        "1. Borrow a book.\n" +
        "2. Return a book.\n" +
        "\n[ADD]\n" +
        "3. Add a student.\n" +
        "4. Add a book.\n" +
        "\n[UPDATE]\n" +
        "5. Update student's grade.\n" +
        "\n[DISPLAY]\n" +
        "6. Display all Students.\n" +
        "7. Display students by grade.\n" +
        "8. Display all books.\n" +
        "9. Display books by year.\n" +
        "\n[FIND]\n" +
        "10. Find Student by id.\n" +
        "11. Find Students by roll number.\n" +
        "12. Find book by id.\n" +
        "13. Find book by title\n" +
        "\n[REMOVE]\n" +
        "14. Remove student by id.\n" +
        "15. Remove book by id.\n" +
        "16. Remove all students.\n" +
        "17. Remove all books.\n" +
        "\n[EXIT]\n" +
        "18. Exit.\n"
        );

    string intErrTxt = "Input must be an integer, try again: ";

    choice = Prompter.PromptGeneric<int>($"Input a number (1-{methodAmout}): ", intErrTxt);

    while (choice < 1 || choice > methodAmout)
    {
        choice = Prompter.PromptGeneric<int>($"Input must be a number 1-{methodAmout}. try again: ", intErrTxt);
    }

    switch (choice)
    {
        case 1: BookMenuActions.BorrowBook(); break;
        case 2: BookMenuActions.ReturnBook(); break;
        case 3: StudentMenuActions.AddStudentAction(); break;
        case 4: BookMenuActions.AddBook(); break;
        case 5: StudentMenuActions.ModifyGrade(); break;
        case 6: StudentManager.DisplayAllStudents(); break;
        case 7: StudentManager.DisplayStudentsByGrade(); break;
        case 8: BookManager.DisplayAllBooks(); break;
        case 9: BookManager.DisplayBooksByYear(); break;
        case 10: StudentMenuActions.FindStudentByIdAction(); break;
        case 11: StudentMenuActions.FindStudentsByRollNum(); break;
        case 12: BookMenuActions.FindBookById(); break;
        case 13: BookMenuActions.FindBookByTitle(); break;
        case 14: StudentMenuActions.RemoveStudentByIdAction(); break;
        case 15: BookMenuActions.RemoveBookById(); break;
        case 16: StudentMenuActions.RemoveAllStudents(); break;
        case 17: BookMenuActions.RemoveAllBooks(); break;
        case 18: break;
    }
} while (choice != methodAmout);