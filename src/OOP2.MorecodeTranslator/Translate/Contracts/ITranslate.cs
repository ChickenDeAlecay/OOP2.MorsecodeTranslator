namespace Translate.Contracts;

public interface ITranslate
{
    public string[] UserInput { get; set; }

    public string TranslationSetPath { get; }

    public string[,] TranslationTable { get; set; }
}