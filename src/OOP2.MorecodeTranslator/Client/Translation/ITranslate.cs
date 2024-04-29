namespace MorsecodeTranslator.Core.Translation;

public interface ITranslate
{
    public string[,] TranslationTable { get; set; }

    public void GetUserInput(string? userName);
}