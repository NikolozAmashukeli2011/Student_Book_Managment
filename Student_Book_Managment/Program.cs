using Spectre.Console;
using Student_Book_Managment.Enums;
using Student_Book_Managment.Helpers.Prompters;
using Student_Book_Managment.Helpers.UI.Printers;
using Student_Book_Managment.Helpers.UI.Styles;
using Student_Book_Managment.Services;
using Student_Book_Managment.Services.Managers;
using Student_Book_Managment.Services.MenuActions;
using Student_Book_Managment.Services.StudentsService;
using Student_Managment.Models;

await StudentManager.InitializeAsync(); // InitilizeAsync method reads the data from the json files asynchronously, and refills the collections of manager classes.
await BookManager.InitializeAsync();

MenuAction[] avaliableActions; // storing names of actions that user can run based on the role.
MenuAction actionChoice; // variable storing the method user picked to run.
string intErrTxt = "Input must be an integer, try again: "; // variables storing text that is later passed into Prompter class method(s).
string rolesTxt = "Input your Role (Student or Admin): ";
string rolesRetryTxt = "Input must be a role (Student or Admin), try again: ";
string stringErrTxt = "Input must be a text, try again: ";

ROLES userRole; // Storing the role result that was returned by Prompter.PromptEnum<T>() method.
 
Console.WriteLine("\nPick your Role\n");

Console.WriteLine("Admin.\n" +
                 "Student.\n");

userRole = Prompter.PromptEnum<ROLES>(rolesTxt, rolesRetryTxt, stringErrTxt);

if (userRole == ROLES.Student)
{
    int userId = StudentPrompter.PromptId();

    if (StudentManager.FindStudentById(userId, out Student? chosenStudent))
    {
        Session.CurrentStudent = chosenStudent; // storing this sessions student.
        Console.WriteLine($"Successfully logged in as: {chosenStudent!.Name}.\n");
    }
    else
    {
        Console.WriteLine($"Student with id: {userId} couldn't be found. would you like to change roles, retry, or Exit?\n");
        Console.WriteLine("1. retry\n" + "2. role\n" + "3. Exit\n");

        int nextActionNum = Prompter.PromptGeneric<int>("Input your next action (1-3): ", intErrTxt);

        while (nextActionNum < 1 || nextActionNum > 3)
        {
            nextActionNum = Prompter.PromptGeneric<int>("Input must be your next action, either 1 or 3, try again: ", intErrTxt);
        }

        if (nextActionNum == 1)
        {
            userId = StudentPrompter.PromptId();
        }
        else if (nextActionNum == 2)
        {
            userRole = Prompter.PromptEnum<ROLES>(rolesTxt, rolesRetryTxt, stringErrTxt);
        }
        else
        {
            Environment.Exit(0);
        }
    }
}

avaliableActions = userRole == ROLES.Student ? MenuCatalog.StudentActions : MenuCatalog.AdminActions;

do
{
    actionChoice = PromptPrinter.SelectionPrinter(title: "Pick your action", choices: avaliableActions, converter: MenuCatalog.ToDisplayText, pageSize: avaliableActions.Length);

    if (actionChoice != MenuAction.Exit) // checking if the user's desired method matches with exit methods number. if it doesn't match,
    {                              // we run the action that is stored inside the actions dictionary with key of method of users choice.
        MenuCatalog.Actions[actionChoice]();  // doing this so i dont get KeyNotFoundException.
    }

} while (actionChoice != MenuAction.Exit);