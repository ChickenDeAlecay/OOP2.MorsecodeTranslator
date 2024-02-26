namespace Morsecode_Translator.Implementations;

using Menu;

//TEST

public static class Program
{
    private static void Main(string[] args)
    {
        bool exit;
        do
        {
            var menuOption = DisplayMenu.CreateMenu(new[] { "American", "International", "Exit" });

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

    private static void American()
    {
        bool exit;
        do
        {
            var menuOption = DisplayMenu.CreateMenu(new[] { "Translate", "Train", "Exit" });

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

    private static void International()
    {
        bool exit;
        do
        {
            var menuOption = DisplayMenu.CreateMenu(new[] { "Translate", "Train", "Exit" });

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
}