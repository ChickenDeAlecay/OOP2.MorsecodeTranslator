namespace Client;

using Client.Training;
using Client.Translation;
using Client.UserAccountManagement;
using Menu;
using Resources;
using Translate.Contracts;
using Translate.Implementations;
using User;

public static class Program
{
    private static void Main(string[] args)
    {
        Program.InitiateProgram();

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
        var translationSetAmerican = new ReadTranslationSet("Translation Sets\\american.txt");
        var translationSetInternatioanl = new ReadTranslationSet("Translation Sets\\international.txt");
        bool exit;
        do
        {
            Console.Clear();

            var menuOption = DisplayMenu.CreateMenu(new[] { "Translate", "Train" });

            switch (menuOption)
            {
                case 1:
                    Program.Translate(user, translationSetInternatioanl, translationSetAmerican);
                    exit = false;
                    break;
                case 2:
                    Program.Train(user, translationSetInternatioanl, translationSetAmerican);
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

    private static void Translate(User user, ReadTranslationSet translationSetInternatioanl,
        ReadTranslationSet translationSetAmerican)
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
                    translate = new TranslateToMorsecode(translationSetInternatioanl);
                    translate.GetUserInput(user.Name);
                    exit = false;
                    break;
                case 2:
                    translate = new TranslateToMorsecode(translationSetAmerican);
                    translate.GetUserInput(user.Name);
                    exit = false;
                    break;
                case 3:
                    translate = new TranslateFromMorsecode(translationSetInternatioanl);
                    translate.GetUserInput(user.Name);
                    exit = false;
                    break;
                case 4:
                    translate = new TranslateFromMorsecode(translationSetAmerican);
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

    private static void Train(User user, ReadTranslationSet translationSetInternatioanl,
        ReadTranslationSet translationSetAmerican)
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
                    training = new TrainMorsecode(translationSetInternatioanl);
                    training.Train(user.Name);
                    exit = false;
                    break;
                case 2:
                    training = new TrainMorsecode(translationSetAmerican);
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

    private static void InitiateProgram()
    {
        if (Directory.Exists("Users") == false)
        {
            Directory.CreateDirectory("Users");
            File.Create("Users\\Logins.csv");
            //File.Create("Users\\Admin.csv");
            File.AppendAllLines("Users\\Admin.csv", new List<string>());

            Console.WriteLine("No Admin Accounts found\nPress any key to continue to create acount");
            Console.ReadKey();
            UserAccountRegister.RegisterUser("Users\\Admin.csv", "");
        }

        if (Directory.Exists("Logs") == false) Directory.CreateDirectory("Logs");

        if (Directory.Exists("Translation Sets") == false)
        {
            var translationsetPath =
                Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.Parent!.FullName,
                    "Client\\Translation Sets");
            Directory.CreateDirectory("Translation Sets");
            File.Copy(translationsetPath + "\\american.txt", "Translation Sets\\american.txt");
            File.Copy(translationsetPath + "\\international.txt", "Translation Sets\\international.txt");
        }
    }
}