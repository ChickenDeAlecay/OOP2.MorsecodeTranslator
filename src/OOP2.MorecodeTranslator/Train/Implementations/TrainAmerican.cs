namespace Train.Implementations;

using Resources;
using Train.Contracts;

public class TrainAmerican : ITrain
{
    public string TranslationSet { get; set; } =
        "C:\\Users\\alecj\\OneDrive - UWE Bristol\\Year2\\OOP2\\Morsecode Translator\\src\\OOP2.MorecodeTranslator\\Resources\\Translation Sets\\american.txt";

    public void Train()
    {
        var translationTable = ReadTranslationSet.GetTranslationSet(TranslationSet);
    }
}