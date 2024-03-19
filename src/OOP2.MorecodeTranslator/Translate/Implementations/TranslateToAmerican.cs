namespace Translate.Implementations;

using System.Text;
using Encryption;
using Logger;
using Resources;
using Translate.Contracts;
using AEncoding;

public class TranslateToAmerican : ITranslate
{
    public TranslateToAmerican(ReadTranslationSet translationSet)
    {
        this.TranslationTable = translationSet.TranslationSet;
    }

    public string TranslationSetPath => "Translation Sets\\american.txt";

    public string[,] TranslationTable { get; set; }

    public void GetUserInput(string? userName)
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
            else if (!this.CheckValidLetter(keyInfo.KeyChar, key))
            {
                Console.Write("\b \b");
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                usersString += keyInfo.KeyChar;
            }
        } while (key != ConsoleKey.Enter);

        var characterSet = string.Empty;

        for (int i = 0; i < TranslationTable.Length/2; i++)
        {
            characterSet += TranslationTable[i,0];
        }

        var aesKey = Console.ReadLine();

        var message = Encrypt.EncryptMessage(usersString.ToUpper(), aesKey);

        Console.WriteLine("\n" + message);

        message = EncodeMessage.Encode(message, characterSet);

        message = this.TranslateToMorsecode(message);

        CreateLog.Log(userName, message);

        Console.ReadKey();
    }

    private bool CheckValidLetter(char keyInfo, ConsoleKey key)
    {
        if (key == ConsoleKey.Spacebar) return true;
        foreach (var letter in this.TranslationTable)
            if (Equals(keyInfo.ToString().ToUpper(), letter))
            {
                return true;
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

            if (inputString == " ") translatedMorsecode.Add("| ");

            for (var i = 0; i < this.TranslationTable.Length / 2 - 1; i++)
                if (inputString == this.TranslationTable[i, 0])
                {
                    translatedMorsecode.Add(this.TranslationTable[i, 1]);
                    break;
                }
        }

        return string.Join("", translatedMorsecode);
    }
}