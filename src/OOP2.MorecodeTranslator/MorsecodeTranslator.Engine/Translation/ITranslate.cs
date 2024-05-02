namespace MorsecodeTranslator.Engine.Translation;

public interface ITranslate
{
    public string[,] TranslationTable { get; set; }

    public string ProcessData(string message, string key);
}