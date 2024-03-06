namespace Translate.Implementations;

using Logger;
using Resources;
using Translate.Contracts;

public class TranslateToInternational : ITranslate
{
    public TranslateToInternational()
    {
        this.TranslationTable = ReadTranslationSet.GetTranslationSet(this.TranslationSetPath);
    }

    public string TranslationSetPath => "Translation Sets\\international.txt";

    public string[,] TranslationTable { get; set; }

    public void GetUserInput(string userName)
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

        var translatedMessage = TranslateToMorsecode(usersString.ToUpper());

        Console.WriteLine("\n" + translatedMessage);

        CreateLog.Log(userName,translatedMessage);

        Console.ReadKey();
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

    private string TranslateToMorsecode(string userMessage)
    {
        var userMessageArray = userMessage.ToCharArray();
        var translatedMorsecode = new List<string?>();
        foreach (var inputChar in userMessageArray)
        {
            var inputString = inputChar.ToString();

            if (inputString == " ")
            {
                translatedMorsecode.Add("|");
                continue;
            }

            for (int i = 0; i < TranslationTable.Length / 2 - 1; i++)
            {
                if (inputString == this.TranslationTable[i, 0])
                {
                    translatedMorsecode.Add(this.TranslationTable[i, 1]);
                    translatedMorsecode.Add(" ");
                    break;
                }
            }
        }

        return string.Join("", translatedMorsecode);
    }
}