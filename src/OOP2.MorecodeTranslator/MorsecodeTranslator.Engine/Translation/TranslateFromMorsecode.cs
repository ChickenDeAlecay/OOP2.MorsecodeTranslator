namespace MorsecodeTranslator.Engine.Translation;

using System.Text;
using MorsecodeTranslator.Engine.Compression;
using MorsecodeTranslator.Engine.Encryption;

public class TranslateFromMorsecode(ReadTranslationSet translationSet) : ITranslate
{
    public string[,] TranslationTable { get; set; } = translationSet.TranslationSet;

    public void GetUserInput(string? userName)
    {
        do
        {
            Console.Clear();

            var directories = Directory.GetDirectories("Logs")
                                       .Where(dir => !dir.EndsWith("Admin"))
                                       .ToArray();

            var selectedDirectory = DisplayMenu.CreateMenu(directories);

            if (selectedDirectory == directories.Length + 1) break;
            if (selectedDirectory > directories.Length + 1) continue;

            Console.Clear();

            var files = Directory.GetFiles(directories[selectedDirectory - 1]);

            var selectedFile = DisplayMenu.CreateMenu(files);

            if (selectedFile == files.Length + 1) break;
            if (selectedFile > files.Length + 1) continue;

            var message = File.ReadAllText(files[selectedFile - 1]);

            message = this.Translate(message);

            Console.WriteLine("Enter a key for your message decryption");

            var aesKey = Console.ReadLine();

            while (string.IsNullOrEmpty(aesKey))
            {
                Console.WriteLine("Invalid input. Please enter a non-empty key.");
                aesKey = Console.ReadLine();
            }

            var decryptedMessage = Decrypt.DecryptMessage(Convert.FromHexString(message), aesKey);

            var decompressedMessage = Decompress.DecompressBytes(Encoding.UTF8.GetBytes(decryptedMessage));

            Console.WriteLine(Encoding.UTF8.GetString(decompressedMessage));

            Console.ReadKey();
        } while (true);
    }

    private string Translate(string message)
    {
        var messageArray = message.Split("  ");
        var translatedMorsecode = string.Empty;
        foreach (var morsecode in messageArray)
        {
            if (morsecode == "|") translatedMorsecode += " ";

            for (var i = 0; i <= this.TranslationTable.Length / 2 - 1; i++)
                if (morsecode == this.TranslationTable[i, 1])
                {
                    translatedMorsecode += this.TranslationTable[i, 0];
                    break;
                }
        }

        return translatedMorsecode;
    }
}