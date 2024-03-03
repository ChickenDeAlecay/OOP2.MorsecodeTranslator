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

            var menuOption = DisplayMenu.CreateMenu(new[] { "Login", "Register", "Exit" });

            switch (menuOption)
            {
                case 1:
                    if (UserAccountLogin.LoginUser()) Program.SelectSet();
                    exit = false;
                    break;
                case 2:
                    UserAccountRegister.RegisterUser();
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

    private static void SelectSet()
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