namespace Translate.Implementations;

using Resources;
using Translate.Contracts;

public class TranslateToAmerican : ITranslate
{
    public TranslateToAmerican()
    {
        this.TranslationTable = ReadTranslationSet.GetTranslationSet(this.TranslationSetPath);
    }

    public string TranslationSetPath => "Translation Sets\\american.txt";

    public string[,] TranslationTable { get; set; }

    public void GetUserInput()
    {
        var usersString = string.Empty;
        ConsoleKey key;

        Console.WriteLine("Enter the string you would like to translate to morsecode");

        do
        {
            var keyInfo = Console.ReadKey(false);
            key = keyInfo.Key;

            if (key == ConsoleKey.Backspace && usersString.Length > 0)
            {
                Console.Write("\b \b");
                usersString = usersString.Remove(usersString.Length - 1, 1);
            }
            else if (!CheckValidLetter(keyInfo.KeyChar, key))
            {
                Console.Write("\b \b");
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                usersString += keyInfo.KeyChar;
            }
        } while (key != ConsoleKey.Enter);

    }

    private bool CheckValidLetter(char keyInfo, ConsoleKey key)
    {
        if (key == ConsoleKey.Spacebar) return true;
        foreach (var letter in this.TranslationTable)
        {
            if (keyInfo.ToString().ToUpper() == letter)
            {
                return true;
            }
        }
        return false;
    }

    private void TranslateToMorsecode(string userMessage)
    {
        var userMessageArray = userMessage.Split();
    }
}