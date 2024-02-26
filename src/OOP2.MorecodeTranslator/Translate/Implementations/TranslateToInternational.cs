namespace Translate.Implementations;

using Resources;

public class TranslateToInternational
{
    public string[] UserInput { get; set; }

    private string TranslationSetPath { get; } =
        "C:\\Users\\alecj\\OneDrive - UWE Bristol\\Year2\\OOP2\\Morsecode Translator\\src\\OOP2.MorecodeTranslator\\Resources\\Translation Sets\\international.txt";

    public string[,] TranslationTable { get; set; }

    public TranslateToInternational()
    {
        this.TranslationTable = ReadTranslationSet.GetTranslationSet(this.TranslationSetPath);
        this.UserInput = this.GetUserInput();
    }

    private string[] GetUserInput()
    {
        Console.WriteLine("Enter the string you would like to translate to morsecode");
        var usersString = Console.ReadLine();
        return usersString.Split();
    }
}