namespace Client;

using Menu;
using Train.Contracts;
using Train.Implementations;
using Translate.Contracts;
using Translate.Implementations;
using User;

//TEST

public static class Program
{
    private static void Main(string[] args)
    {
        bool exit;
        do
        {
            Console.Clear();

            var menuOption = DisplayMenu.CreateMenu(new[] { "User Login", "Admin Login" });

            User user;

            switch (menuOption)
            {
                case 1:
                    user = UserAccountLogin.LoginUser("Users\\Logins.csv");
                    if (user.Created) Program.SelectMode(user);
                    exit = false;
                    break;
                case 2:
                    user = UserAccountLogin.LoginUser("Users\\Admin.csv");
                    if (user.Created) Program.AdminManagement(user);
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

    private static void AdminManagement(User user)
    {
        bool exit;
        do
        {
            Console.Clear();

            var menuOption = DisplayMenu.CreateMenu(new[]
            {
                "Create New User", "Change User Details", "Delete User Account", "Create New Admin",
                "Change Admin Details",
                "Delete Admin Account"
            });

            switch (menuOption)
            {
                case 1:
                    UserAccountRegister.RegisterUser("Users\\Logins.csv", user.Name);
                    exit = false;
                    break;
                case 2:
                    UserAccountEdit.EditUser("Users\\Logins.csv", user.Name);
                    exit = false;
                    break;
                case 3:
                    UserAccountDelete.DeleteUser("Users\\Logins.csv", user.Name);
                    exit = false;
                    break;
                case 4:
                    UserAccountRegister.RegisterUser("Users\\Admin.csv", user.Name);
                    exit = false;
                    break;
                case 5:
                    UserAccountEdit.EditUser("Users\\Admin.csv", user.Name);
                    exit = false;
                    break;
                case 6:
                    UserAccountDelete.DeleteUser("Users\\Admin.csv", user.Name);
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

    private static void SelectMode(User user)
    {
        bool exit;
        do
        {
            Console.Clear();

            var menuOption = DisplayMenu.CreateMenu(new[] { "Translate", "Train" });

            switch (menuOption)
            {
                case 1:
                    Program.Translate(user);
                    exit = false;
                    break;
                case 2:
                    Program.Train(user);
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

    private static void Translate(User user)
    {
        bool exit;
        do
        {
            Console.Clear();
            var menuOption = DisplayMenu.CreateMenu(new[]
            {
                "Text to International", "Text to American", "Morsecode to International", "Morsecode to American"
            });
            ITranslate translate;

            switch (menuOption)
            {
                case 1:
                    translate = new TranslateToInternational();
                    translate.GetUserInput(user.Name);
                    exit = false;
                    break;
                case 2:
                    translate = new TranslateToAmerican();
                    translate.GetUserInput(user.Name);
                    exit = false;
                    break;
                case 3:
                    translate = new TranslateFromInternational();
                    translate.GetUserInput(user.Name);
                    exit = false;
                    break;
                case 4:
                    translate = new TranslateFromAmerican();
                    translate.GetUserInput(user.Name);
                    exit = false;
                    break;
                case 5:
                    exit = true;
                    break;
                default:
                    exit = false;
                    break;
            }
        } while (exit == false);
    }

    private static void Train(User user)
    {
        bool exit;
        do
        {
            Console.Clear();
            var menuOption = DisplayMenu.CreateMenu(new[] { "International", "American" });
            ITrain training;

            switch (menuOption)
            {
                case 1:
                    training = new TrainInternational();
                    training.Train(user.Name);
                    exit = false;
                    break;
                case 2:
                    training = new TrainAmerican();
                    training.Train(user.Name);
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