namespace MorsecodeTranslator.Engine.UserConsoleInput;

public static class GetUserInput
{
    public static string GetUserMessage()
    {
        Console.WriteLine("Enter the string you would like to translate to morsecode");

        var usersString = Console.ReadLine();

        while (string.IsNullOrEmpty(usersString))
        {
            Console.WriteLine("Invalid input. Please enter a string.");
            usersString = Console.ReadLine();
        }

        return usersString;
    }

    public static string GetUserKey()
    {
        Console.WriteLine("Enter a key for your message encryption");

        var aesKey = Console.ReadLine();

        while (string.IsNullOrEmpty(aesKey))
        {
            Console.WriteLine("Invalid input. Please enter a key.");
            aesKey = Console.ReadLine();
        }

        return aesKey;
    }
}