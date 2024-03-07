namespace Menu;

public static class DisplayMenu
{
    public static int CreateMenu(string[] menuOptions)
    {
        var iteration = 1;
        foreach (var menuOption in menuOptions)
        {
            Console.WriteLine($"{iteration}. {menuOption}");
            iteration++;
        }

        Console.WriteLine($"{iteration}. Exit");

        string? menuSelection;
        int menuSelectionInt;
        do
        {
            Console.Write("Enter Selection: ");
            menuSelection = Console.ReadLine();
        } while (!int.TryParse(menuSelection, out menuSelectionInt) || menuSelectionInt < 1 ||
                 menuSelectionInt > menuOptions.Length + 1);

        return menuSelectionInt;
    }
}