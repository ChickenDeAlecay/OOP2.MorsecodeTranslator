namespace Train.Implementations;

using Resources;
using Train.Contracts;

public class TrainAmerican : ITrain
{
    public TrainAmerican()
    {
        this.TranslationTable = ReadTranslationSet.GetTranslationSet(TranslationSet);
    }

    public string TranslationSet { get; set; } =
        "C:\\Users\\alecj\\OneDrive - UWE Bristol\\Year2\\OOP2\\Morsecode Translator\\src\\OOP2.MorecodeTranslator\\Resources\\Translation Sets\\american.txt";

    public string[,] TranslationTable { get; set; }
    public string[] TrainingResults { get; set; } = new string[15];


    public void Train()
    {
        Console.Clear();
        Console.WriteLine("Morsecode Training - American:");

        Random random = new Random();

        for (int i = 0; i < 15; i++)
        {
            var randomIndex = random.Next(0, 35);
            Console.WriteLine($"Question {i}. What is {this.TranslationTable[randomIndex,1]}");
            Console.Write("Enter Answer: ");
            var answer = Console.ReadLine().ToUpper();
            RecordResult(answer, randomIndex, i);
        }

        Console.Clear();
        foreach (var result in this.TrainingResults)
        {
            Console.WriteLine(result);
        }

        Console.Write("Press any key to continue.");
        Console.ReadKey();
    }

    private void RecordResult(string answer, int index, int i)
    {
        if (this.TranslationTable[index,0] == answer)
        {
            this.TrainingResults[i] = $"Question {i+1}. {this.TranslationTable[index,1]} = {this.TranslationTable[index,0]} - Correct";
        }
        else
        {
            this.TrainingResults[i] = $"Question {i + 1}. {this.TranslationTable[index, 1]} = {this.TranslationTable[index, 0]} - Wrong - Your answer was {answer}";
        }
    }
}