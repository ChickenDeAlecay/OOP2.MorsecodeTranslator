namespace Translate.Contracts;

public interface ITranslate
{
    public string TranslationSetPath { get; }

    public string[,] TranslationTable { get; set; }

    public void GetUserInput(string userName);
}