namespace MorsecodeTranslator.Engine;

public class ReadTranslationSet
{
    public string[,] TranslationSet { get; set; }

    public ReadTranslationSet(string filePath)
    {
        this.TranslationSet = this.GetTranslationSet(filePath);
    }

    public string[,] GetTranslationSet(string filePath)
    {
        var translationSet = new string[35, 2];

        var translationChar = File.ReadLines(filePath).Skip(6).Take(25).ToList();
        var translationNum = File.ReadLines(filePath).Skip(33).Take(10).ToList();

        var iteration = 0;
        foreach (var lines in translationChar)
        {
            var alphabetTranslations = lines.Split([' '], 2);

            translationSet[iteration, 0] = alphabetTranslations[0];
            translationSet[iteration, 1] = alphabetTranslations[1];

            iteration += 1;
        }

        foreach (var lines in translationNum)
        {
            var numberTranslations = lines.Split([' '], 2);

            translationSet[iteration, 0] = numberTranslations[0];
            translationSet[iteration, 1] = numberTranslations[1];

            iteration += 1;
        }

        return translationSet;
    }
}