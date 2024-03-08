namespace Translate.Implementations;

using System.Security.Cryptography;
using Encryption;
using Menu;
using Resources;
using Translate.Contracts;

public class TranslateFromAmerican : ITranslate
{
    public TranslateFromAmerican()
    {
        this.TranslationTable = ReadTranslationSet.GetTranslationSet(this.TranslationSetPath);
    }

    public string TranslationSetPath => "Translation Sets\\american.txt";
    public string[,] TranslationTable { get; set; }

    public void GetUserInput(string userName)
    {
        do
        {
            var directories = Directory.GetDirectories("Logs");

            var selectedDirectory = DisplayMenu.CreateMenu(directories);

            if (selectedDirectory == directories.Length + 1) break;
            if (selectedDirectory > directories.Length + 1) continue;

            Console.Clear();

            var files = Directory.GetFiles(directories[selectedDirectory - 1]);

            var selectedFile = DisplayMenu.CreateMenu(files);

            if (selectedFile == files.Length + 1) break;
            if (selectedFile > files.Length + 1) continue;

            var message = File.ReadAllText(files[selectedFile - 1]);

            message = Decrypt.DecryptMessage(message);

            Console.WriteLine(this.TranslateFromMorsecode(message));
        } while (true);
    }

    private List<string?> TranslateFromMorsecode(string message)
    {
        var messageArray = message.Split(' ');
        var translatedMorsecode = new List<string?>();
        foreach (var morsecode in messageArray)
        {
            if (morsecode == "| ") translatedMorsecode.Add(" ");

            for (var i = 0; i < this.TranslationTable.Length / 2 - 1; i++)
                if (morsecode == this.TranslationTable[i, 1])
                {
                    translatedMorsecode.Add(this.TranslationTable[i, 0]);
                    break;
                }
        }

        return translatedMorsecode;
    }
}