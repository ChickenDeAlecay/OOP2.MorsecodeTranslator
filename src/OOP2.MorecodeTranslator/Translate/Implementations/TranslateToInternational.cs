namespace Translate.Implementations;

using AEncoding;
using Encryption;
using Logger;
using Resources;
using Translate.Contracts;

public class TranslateToInternational : ITranslate
{
    public TranslateToInternational(ReadTranslationSet translationSet)
    {
        this.TranslationTable = translationSet.TranslationSet;
    }

    public string[,] TranslationTable { get; set; }

    public void GetUserInput(string? userName)
    {
        var usersString = string.Empty;
        ConsoleKey key;

        Console.WriteLine("Enter the string you would like to translate to morsecode");

        //do
        //{
        //    var keyInfo = Console.ReadKey(false);
        //    key = keyInfo.Key;

        //    if (key == ConsoleKey.Backspace && usersString.Length > 0)
        //    {
        //        Console.Write("\b \b");
        //        usersString = usersString.Remove(usersString.Length - 1, 1);
        //    }
        //    else if (!this.CheckValidLetter(keyInfo.KeyChar, key))
        //    {
        //        Console.Write("\b \b");
        //    }
        //    else if (!char.IsControl(keyInfo.KeyChar))
        //    {
        //        usersString += keyInfo.KeyChar;
        //    }
        //} while (key != ConsoleKey.Enter);

        usersString = Console.ReadLine();

        var characterSet = string.Empty;

        for (var i = 0; i < this.TranslationTable.Length / 2; i++) characterSet += this.TranslationTable[i, 0];

        var aesKey = Console.ReadLine();

        var message = Encrypt.EncryptMessage(usersString.ToUpper(), aesKey);

        Console.WriteLine("\n" + message);

        message = EncodeMessage.Encode(message, characterSet);

        Console.WriteLine("\n" + message);

        message = this.TranslateToMorsecode(message);

        Console.WriteLine("\n" + message);

        CreateLog.Log(userName, message);

        Console.ReadKey();
    }

    private bool CheckValidLetter(char keyInfo, ConsoleKey key)
    {
        if (key == ConsoleKey.Spacebar) return true;
        foreach (var letter in this.TranslationTable)
            if (string.Equals(keyInfo.ToString().ToUpper(), letter))
                return true;
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
                    translatedMorsecode.Add(" ");
                    break;
                }
        }

        return string.Join("", translatedMorsecode);
    }
}