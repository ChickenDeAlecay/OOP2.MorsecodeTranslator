namespace MorsecodeTranslator.Engine.Translation;

using System.Text;
using MorsecodeTranslator.Engine.Compression;
using MorsecodeTranslator.Engine.Encryption;

public class TranslateToMorsecode(ReadTranslationSet translationSet) : ITranslate
{
    public string[,] TranslationTable { get; set; } = translationSet.TranslationSet;

    public void GetUserInput(string? userName)
    {
        Console.WriteLine("Enter the string you would like to translate to morsecode");

        var usersString = Console.ReadLine();

        while (string.IsNullOrEmpty(usersString))
        {
            Console.WriteLine("Invalid input. Please enter a non-empty string.");
            usersString = Console.ReadLine();
        }

        Console.WriteLine("Enter a key for your message encryption");

        var aesKey = Console.ReadLine();

        while (string.IsNullOrEmpty(aesKey))
        {
            Console.WriteLine("Invalid input. Please enter a non-empty key.");
            aesKey = Console.ReadLine();
        }

        var message = this.ProcessData(usersString, aesKey);

        var logLocation = CreateLog.Log(userName, message);

        Console.WriteLine("Your message:\n" + message + "\nThis has been logged at " + logLocation);
        Console.Write("Press any key to continue");
        Console.ReadKey();
    }

    public string ProcessData(string origionalMessage, string encryptionKey)
    {
        var bytes = Encoding.UTF8.GetBytes(origionalMessage);

        var compressedMessage = Compress.CompressBytes(bytes);

        var encryptedMessage = Encrypt.EncryptMessage(compressedMessage, encryptionKey);

        var processedMessage = this.Translate(Convert.ToHexString(encryptedMessage));

        return processedMessage;
    }

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