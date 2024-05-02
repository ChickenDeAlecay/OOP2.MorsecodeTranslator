namespace MorsecodeTranslator.Engine.Translation;

using System.Text;
using MorsecodeTranslator.Engine.Compression;
using MorsecodeTranslator.Engine.Encryption;

public class TranslateToMorsecode : ITranslate
{
    private readonly IAesEncryption encrypt;
    private readonly IGzip gzip;

    public TranslateToMorsecode(ReadTranslationSet translationSet, IGzip gzip, IAesEncryption encrypt)
    {
        this.TranslationTable = translationSet.TranslationSet;
        this.gzip = gzip;
        this.encrypt = encrypt;
    }

    public string[,] TranslationTable { get; set; }

    public string ProcessData(string origionalMessage, string encryptionKey)
    {
        var bytes = Encoding.UTF8.GetBytes(origionalMessage);

        var compressedMessage = this.gzip.CompressBytes(bytes);

        var encryptedMessage = this.encrypt.EncryptMessage(compressedMessage, encryptionKey);

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