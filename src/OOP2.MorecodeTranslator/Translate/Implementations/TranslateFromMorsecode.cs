using AEncoding;
using Encryption;
using Menu;
using Resources;

namespace Translate.Implementations;

using Translate.Contracts;

public class TranslateFromMorsecode : ITranslate
{
    public TranslateFromMorsecode(ReadTranslationSet translationSet)
    {
        this.TranslationTable = translationSet.TranslationSet;
    }

    public string[,] TranslationTable { get; set; }

    public void GetUserInput(string? userName)
    {
        do
        {
            Console.Clear();

            var directories = Directory.GetDirectories("Logs");

            var selectedDirectory = DisplayMenu.CreateMenu(directories);

            if (selectedDirectory == directories.Length + 1) break;
            if (selectedDirectory > directories.Length + 1) continue;

            Console.Clear();

            var files = Directory.GetFiles(directories[selectedDirectory - 1]);

            var selectedFile = DisplayMenu.CreateMenu(files);

            if (selectedFile == files.Length + 1) break;
            if (selectedFile > files.Length + 1) continue;

            var characterSet = string.Empty;

            for (var i = 0; i < this.TranslationTable.Length / 2; i++) characterSet += this.TranslationTable[i, 0];

            var message = File.ReadAllText(files[selectedFile - 1]);

            message = this.TranslateFromMorsecode(message);

            var decodedMessage = DecodeMessage.Decode(message, characterSet);

            var aesKey = Console.ReadLine();

            var decryptedMessage = Decrypt.DecryptMessage(decodedMessage, aesKey);

            Console.WriteLine(decryptedMessage);
        } while (true);
    }

    private string TranslateFromMorsecode(string message)
    {
        var messageArray = message.Split(' ');
        var translatedMorsecode = string.Empty;
        foreach (var morsecode in messageArray)
        {
            if (morsecode == "|") translatedMorsecode += " ";

            for (var i = 0; i < this.TranslationTable.Length / 2 - 1; i++)
                if (morsecode == this.TranslationTable[i, 1])
                {
                    translatedMorsecode += this.TranslationTable[i, 0];
                    break;
                }
        }

        return translatedMorsecode;
    }
}