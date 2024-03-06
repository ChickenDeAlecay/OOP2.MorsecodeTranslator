namespace Translate.Implementations;

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
        throw new NotImplementedException();
    }
}