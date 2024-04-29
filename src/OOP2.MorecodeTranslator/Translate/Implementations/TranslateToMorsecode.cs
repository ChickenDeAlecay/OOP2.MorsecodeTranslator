namespace Translate.Implementations;

using System.Security.Cryptography;
using Encryption;
using Logger;
using Resources;
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

        using var myAes = Aes.Create();

        var aesKey = Console.ReadLine();

        var encryptedMessage = Encrypt.EncryptMessage(usersString, aesKey);

        Console.WriteLine(Convert.ToHexString(encryptedMessage));

        //var encodedMessage = EncodeMessage.Encode(encryptedMessage, characterSet);

        //Console.WriteLine(encodedMessage);

        var message = this.Translate(Convert.ToHexString(encryptedMessage));

        Console.WriteLine(message);

        CreateLog.Log(userName, message);

        Console.ReadKey();
    }

    //TODO: make it so that the space between morsecode is "  " to be able to use American translation set
    private string Translate(string userMessage)
    {
        var userMessageArray = userMessage.ToCharArray();
        var translatedMorsecode = new List<string?>();
        foreach (var inputChar in userMessageArray)
        {
            var inputString = inputChar.ToString();

            if (inputString == " ") translatedMorsecode.Add("|  ");

            for (var i = 0; i <= this.TranslationTable.Length / 2 - 1; i++)
                if (inputString == this.TranslationTable[i, 0])
                {
                    translatedMorsecode.Add(this.TranslationTable[i, 1]);
                    translatedMorsecode.Add("  ");
                    break;
                }
        }

        return string.Join("", translatedMorsecode);
    }
}