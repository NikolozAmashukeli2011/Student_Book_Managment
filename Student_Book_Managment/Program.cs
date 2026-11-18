using Student_Book_Managment.Enums;
using Student_Book_Managment.Services.Managers;
using Student_Book_Managment.Services.MenuActions;
using Student_Book_Managment.Services.StudentsService;
using Student_Managment.Helpers;

await StudentManager.InitializeAsync(); // InitilizeAsync method reads the data from the json files, and refills the collections.
await BookManager.InitializeAsync();

int methodChoice; // integer variable storing the method user picked to run.
int exitChoice; // integer variable that stores the last method's (which is always exit) number.
string intErrTxt = "Input must be an integer, try again: "; // variable storing text that is later passed into Prompter class method(s).
string rolesTxt = "Input your Role (Student or Admin): "; // same thing here, variable storing that that gets passed into Prompter class method(s).
string rolesRetryTxt = "Input must be a role (Student or Admin), try again: ";// same here....
string stringErrTxt = "Input must be a text, try again: "; // same here...

Dictionary<int, Action> actions; // Dictionary storing methods number as the key, and method (Action) reference itself as a value.
                                 // using this so program doesn't asks about the roles to then match the runnable methods on each iteration.
Roles userRole; // Storing the result that was returned by Prompter.PromptEnum<T>() method.

string textBasedOnRole = string.Empty; // Variable storing text that is supposed to be displayed on each iteration based on user role (what methods he can access).

Console.WriteLine("\nPick your Role\n");

Console.WriteLine("1. Admin.\n" +
                 "2. Student.\n");

userRole = Prompter.PromptEnum<Roles>(rolesTxt, rolesRetryTxt, stringErrTxt);

if (userRole == Roles.Admin)
{
    textBasedOnRole =
        "\n[ADD]\n" +
        "1. Add a student.\n" +
        "2. Add a book.\n" +
        "\n[UPDATE]\n" +
        "3. Update student’s grade.\n" +
        "\n[DISPLAY]\n" +
        "4. Display all students.\n" +
        "5. Display students by grade.\n" +
        "6. Display all books.\n" +
        "7. Display books by year.\n" +
        "\n[FIND]\n" +
        "8. Find student by id.\n" +
        "9. Find students by roll number.\n" +
        "\n[REMOVE]\n" +
        "10. Remove student by id.\n" +
        "11. Remove book by id.\n" +
        "12. Remove all students.\n" +
        "13. Remove all books.\n" +
        "\n[EXIT]\n" +
        "14. Exit.\n";

    actions = new() // Matching the method number to the methods reference.
    {
        [1] = StudentMenuActions.AddStudentAction,
        [2] = BookMenuActions.AddBook,
        [3] = StudentMenuActions.ModifyGrade,
        [4] = StudentManager.DisplayAllStudents,
        [5] = StudentManager.DisplayStudentsByGrade,
        [6] = BookManager.DisplayAllBooks,
        [7] = BookManager.DisplayBooksByYear,
        [8] = StudentMenuActions.FindStudentByIdAction,
        [9] = StudentMenuActions.FindStudentsByRollNum,
        [10] = StudentMenuActions.RemoveStudentByIdAction,
        [11] = BookMenuActions.RemoveBookById,
        [12] = StudentMenuActions.RemoveAllStudents,
        [13] = BookMenuActions.RemoveAllBooks
    };
}
else
{
    textBasedOnRole =
    "\n[LIBRARY]\n" +
    "1. Borrow a book.\n" +
    "2. Return a book.\n" +
    "\n[DISPLAY]\n" +
    "3. Display borrowed books.\n" +
    "4. Display all books.\n" +
    "5. Display books by year.\n" +
    "\n[FIND]\n" +
    "6. Find book by id.\n" +
    "7. Find book by title.\n" +
    "\n[EXIT]\n" +
    "8. Exit.\n";

    actions = new()
    {
        [1] = BookMenuActions.BorrowBook,
        [2] = BookMenuActions.ReturnBook,
        [3] = BookMenuActions.DisplayBorrowedBooks,
        [4] = BookManager.DisplayAllBooks,
        [5] = BookManager.DisplayBooksByYear,
        [6] = BookMenuActions.FindBookById,
        [7] = BookMenuActions.FindBookByTitle
    };
}

exitChoice = actions.Count + 1;

do
{
    Console.WriteLine(textBasedOnRole);

    methodChoice = Prompter.PromptGeneric<int>($"Input a number (1-{exitChoice}): ", intErrTxt);

    while (methodChoice < 1 || methodChoice > exitChoice)
    {
        methodChoice = Prompter.PromptGeneric<int>($"Input must be a number 1-{exitChoice}, try again: ", intErrTxt);
    }

    if (methodChoice != exitChoice) // checking if the user's desired method matches with exit methods number. if it doesn't match,
                                    // we run the action that is stored inside the actions dictionary with key of method of users choice.
                                    // doing this so i dont get KeyNotFoundException.
    {
        actions[methodChoice]();
    }


} while (methodChoice != exitChoice);