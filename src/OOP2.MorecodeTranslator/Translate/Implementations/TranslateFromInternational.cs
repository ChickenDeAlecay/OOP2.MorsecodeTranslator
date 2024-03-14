namespace Translate.Implementations;

using Encryption;
using Menu;
using Resources;
using Translate.Contracts;

public class TranslateFromInternational : ITranslate
{
    public TranslateFromInternational(ReadTranslationSet translationSet)
    {
        this.TranslationTable = translationSet.TranslationSet;
    }

    public string TranslationSetPath => "Translation Sets\\international.txt";
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

            var message = File.ReadAllText(files[selectedFile - 1]);

            message = Decrypt.DecryptMessage(message);

            Console.WriteLine(this.TranslateFromMorsecode(message));
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