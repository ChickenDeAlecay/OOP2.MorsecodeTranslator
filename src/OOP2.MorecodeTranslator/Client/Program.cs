using Menu;
using Train.Contracts;
using Train.Implementations;

namespace Client;

//TEST

public static class Program
{
    private static void Main(string[] args)
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

            switch (menuOption)
            {
                case 1:
                    exit = false;
                    break;
                case 2:
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