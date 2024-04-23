using AEncoding;
using Encryption;
using Logger;
using Resources;

namespace Translate.Implementations;

using Translate.Contracts;

public class TranslateToMorsecode : ITranslate
{
    public TranslateToMorsecode(ReadTranslationSet translationSet)
    {
        this.TranslationTable = translationSet.TranslationSet;
    }

    public string[,] TranslationTable { get; set; }

    public void GetUserInput(string? userName)
    {
        var usersString = string.Empty;
        ConsoleKey key;

        Console.WriteLine("Enter the string you would like to translate to morsecode");

        usersString = Console.ReadLine();

        var characterSet = string.Empty;

        for (var i = 0; i < this.TranslationTable.Length / 2; i++) characterSet += this.TranslationTable[i, 0];
        Console.WriteLine("Enter a key for your message encryption");

        var aesKey = Console.ReadLine();

        var encryptedMessage = Encrypt.EncryptMessage(usersString.ToUpper(), aesKey);

        var encodedMessage = EncodeMessage.Encode(encryptedMessage, characterSet);

        var message = this.Translate(encodedMessage);

        CreateLog.Log(userName, message);

        Console.ReadKey();
    }

    private string Translate(string userMessage)
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