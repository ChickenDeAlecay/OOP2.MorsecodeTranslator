namespace Translate.Implementations;

using Resources;
using Translate.Contracts;

public class TranslateFromInternational : ITranslate
{
    public TranslateFromInternational()
    {
        this.TranslationTable = ReadTranslationSet.GetTranslationSet(this.TranslationSetPath);
    }
    public string TranslationSetPath => "Translation Sets\\international.txt";
    public string[,] TranslationTable { get; set; }
    public void GetUserInput(string userName)
    {
        throw new NotImplementedException();
    }

    public void GetUserInput()
    {
        throw new NotImplementedException();
    }
}