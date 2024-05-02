namespace MorsecodeTranslator.Engine.Translation;

using System.Text;
using MorsecodeTranslator.Engine.Compression;
using MorsecodeTranslator.Engine.Encryption;

public class TranslateFromMorsecode : ITranslate
{
    private readonly IAesEncryption decrypt;
    private readonly IGzip gzip;

    public TranslateFromMorsecode(ReadTranslationSet translationSet, IGzip gzip, IAesEncryption decrypt)
    {
        this.TranslationTable = translationSet.TranslationSet;
        this.gzip = gzip;
        this.decrypt = decrypt;
    }

    public string[,] TranslationTable { get; set; }

    public string ProcessData(string message, string key)
    {
        var translatedMessage = this.Translate(message);

        var decryptedMessage = this.decrypt.DecryptMessage(Convert.FromHexString(translatedMessage), key);

        var decompressedMessage = this.gzip.DecompressBytes(decryptedMessage);

        return Encoding.UTF8.GetString(decompressedMessage);
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