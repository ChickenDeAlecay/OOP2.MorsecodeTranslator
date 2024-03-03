namespace Translate.Implementations;

using Resources;
using Translate.Contracts;

public class TranslateToInternational : ITranslate
{
    public TranslateToInternational()
    {
        this.TranslationTable = ReadTranslationSet.GetTranslationSet(this.TranslationSetPath);
        this.UserInput = this.GetUserInput();
    }

    public string[] UserInput { get; set; }

    public string TranslationSetPath => "Translation Sets\\international.txt";

    public string[,] TranslationTable { get; set; }

    public string[] GetUserInput()
    {
        Console.WriteLine("Enter the string you would like to translate to morsecode");
        var usersString = Console.ReadLine();
        return usersString.Split();
    }
}