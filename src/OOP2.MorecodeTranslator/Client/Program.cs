namespace Client;

using Menu;
using Train.Contracts;
using Train.Implementations;
using Translate.Contracts;
using Translate.Implementations;

//TEST

public static class Program
{
    private static void Main(string[] args)
    {
        bool exit;
        do
        {
            Console.Clear();

            var menuOption = DisplayMenu.CreateMenu(new[] { "User Login", "Admin Login", "Close Program" });

            switch (menuOption)
            {
                case 1:
                    if (UserAccountLogin.LoginUser("Users\\Logins.csv")) Program.SelectMode();
                    exit = false;
                    break;
                case 2:
                    if (UserAccountLogin.LoginUser("Users\\Admin.csv")) Program.AdminManagement();
                    exit = false;
                    break;
                case 3:
                    exit = true;
                    break;
                default:
                    exit = false;
                    break;
            }
        } while (exit == false);
    }

    private static void AdminManagement()
    {
        bool exit;
        do
        {
            Console.Clear();

            var menuOption = DisplayMenu.CreateMenu(new[] 
                { "Create New User", "Change User Details", "Delete User", "Create New Admin", "Change Admin Details", "Delete Admin", "Exit" });

            switch (menuOption)
            {
                case 1:
                    UserAccountRegister.RegisterUser("Users\\Logins.csv");
                    exit = false;
                    break;
                case 2:
                    UserAccountEdit.EditUser("Users\\Logins.csv");
                    exit = false;
                    break;
                case 3:
                    exit = true;
                    break;
                case 4:
                    UserAccountRegister.RegisterUser("Users\\Admin.csv");
                    exit = false;
                    break;
                case 5:
                    UserAccountEdit.EditUser("Users\\Admin.csv");
                    exit = false;
                    break;
                case 6:
                    exit = false;
                    break;
                case 7:
                    exit = true;
                    break;
                default:
                    exit = false;
                    break;
            }
        } while (exit == false);
    }
    private static void SelectMode()
    {
        bool exit;
        do
        {
            Console.Clear();

            var menuOption = DisplayMenu.CreateMenu(new[] { "Translate", "Train", "Exit" });

            switch (menuOption)
            {
                case 1:
                    Program.Translate();
                    exit = false;
                    break;
                case 2:
                    Program.Train();
                    exit = false;
                    break;
                case 3:
                    exit = true;
                    break;
                default:
                    exit = false;
                    break;
            }
        } while (exit == false);
    }

    private static void Translate()
    {
        bool exit;
        do
        {
            Console.Clear();
            var menuOption = DisplayMenu.CreateMenu(new[] { "International", "American", "Exit" });
            ITranslate translate;

            switch (menuOption)
            {
                case 1:
                    translate = new TranslateToInternational();
                    translate.GetUserInput();
                    exit = false;
                    break;
                case 2:
                    translate = new TranslateToAmerican();
                    translate.GetUserInput();
                    exit = false;
                    break;
                case 3:
                    exit = true;
                    break;
                default:
                    exit = false;
                    break;
            }
        } while (exit == false);
    }

    private static void Train()
    {
        bool exit;
        do
        {
            Console.Clear();
            var menuOption = DisplayMenu.CreateMenu(new[] { "International", "American", "Exit" });
            ITrain training;

            switch (menuOption)
            {
                case 1:
                    training = new TrainInternational();
                    training.Train();
                    exit = false;
                    break;
                case 2:
                    training = new TrainAmerican();
                    training.Train();
                    exit = false;
                    break;
                case 3:
                    exit = true;
                    break;
                default:
                    exit = false;
                    break;
            }
        } while (exit == false);
    }
}