using Student_Book_Managment.Services.Managers;
using Student_Book_Managment.Services.MenuActions;
using Student_Book_Managment.Services.StudentsService;
using Student_Managment.Helpers;

await StudentManager.InitializeAsync();
await BookManager.InitializeAsync();

int choice;
int methodAmout = 19;

do
{
    Console.WriteLine(
        "\n[LIBRARY]\n" +
        "1. Borrow a book.\n" +
        "2. Return a book.\n" +
        "3. Display Borrowed Books.\n" +
        "\n[ADD]\n" +
        "4. Add a student.\n" +
        "5. Add a book.\n" +
        "\n[UPDATE]\n" +
        "6. Update student's grade.\n" +
        "\n[DISPLAY]\n" +
        "7. Display all Students.\n" +
        "8. Display students by grade.\n" +
        "9. Display all books.\n" +
        "10. Display books by year.\n" +
        "\n[FIND]\n" +
        "11. Find Student by id.\n" +
        "12. Find Students by roll number.\n" +
        "13. Find book by id.\n" +
        "14. Find book by title\n" +
        "\n[REMOVE]\n" +
        "15. Remove student by id.\n" +
        "16. Remove book by id.\n" +
        "17. Remove all students.\n" +
        "18. Remove all books.\n" +
        "\n[EXIT]\n" +
        "19. Exit.\n"
        );

    string intErrTxt = "Input must be an integer, try again: ";

    choice = Prompter.PromptGeneric<int>($"Input a number (1-{methodAmout}): ", intErrTxt);

    while (choice < 1 || choice > methodAmout)
    {
        choice = Prompter.PromptGeneric<int>($"Input must be a number 1-{methodAmout}. try again: ", intErrTxt);
    }

    switch (choice)
    {
        // [LIBRARY]------------------------------------------
        case 1: BookMenuActions.BorrowBook(); break;
        case 2: BookMenuActions.ReturnBook(); break;
        case 3: BookMenuActions.DisplayBorrowedBooks(); break;
        //---------------------------------------------------

        // [ADD]---------------------------------------------
        case 4: StudentMenuActions.AddStudentAction(); break;
        case 5: BookMenuActions.AddBook(); break;
        //---------------------------------------------------

        // [UPDATE]------------------------------------------
        case 6: StudentMenuActions.ModifyGrade(); break;
        //---------------------------------------------------

        // [DISPLAY]-----------------------------------------
        case 7: StudentManager.DisplayAllStudents(); break;
        case 8: StudentManager.DisplayStudentsByGrade(); break;
        case 9: BookManager.DisplayAllBooks(); break;
        case 10: BookManager.DisplayBooksByYear(); break;
        //---------------------------------------------------

        // [FIND]--------------------------------------------
        case 11: StudentMenuActions.FindStudentByIdAction(); break;
        case 12: StudentMenuActions.FindStudentsByRollNum(); break;
        case 13: BookMenuActions.FindBookById(); break;
        case 14: BookMenuActions.FindBookByTitle(); break;
        //---------------------------------------------------

        // [REMOVE]------------------------------------------
        case 15: StudentMenuActions.RemoveStudentByIdAction(); break;
        case 16: BookMenuActions.RemoveBookById(); break;
        case 17: StudentMenuActions.RemoveAllStudents(); break;
        case 18: BookMenuActions.RemoveAllBooks(); break;
        //---------------------------------------------------

        // [EXIT]--------------------------------------------
        case 19: break;
            //---------------------------------------------------
    }
} while (choice != methodAmout);