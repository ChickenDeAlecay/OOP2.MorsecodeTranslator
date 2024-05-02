namespace MorsecodeTranslator.Engine.Training;

using MorsecodeTranslator.Engine.Logging;

public class TrainMorsecode : ITrain
{
    private readonly ILog log;

    public TrainMorsecode(ReadTranslationSet translationSet, ILog log)
    {
        this.TranslationTable = translationSet.TranslationSet;
        this.log = log;
    }

    public string[,] TranslationTable { get; set; }
    public string[] TrainingResults { get; set; } = new string[15];

    public void Train(string? name)
    {
        Console.Clear();
        Console.WriteLine("Morsecode Training:\n");

        var random = new Random();
        var generatedInts = new List<int>();

        for (var i = 0; i < 15; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = random.Next(0, 35);
            } while (generatedInts.Contains(randomIndex));

            generatedInts.Add(randomIndex);

            Console.WriteLine($"Question {i}. What is {this.TranslationTable[randomIndex, 1]}");
            Console.Write("Enter Answer: ");
            var answer = Console.ReadLine()!.ToUpper();
            this.RecordResult(answer, randomIndex, i);
        }

        Console.Clear();
        foreach (var result in this.TrainingResults) Console.WriteLine(result);

        this.log.CreateLog(name, $"{this.TrainingResults}");

        Console.Write("Press any key to continue.");
        Console.ReadKey();
    }

    private void RecordResult(string answer, int index, int i)
    {
        if (this.TranslationTable[index, 0] == answer)
            this.TrainingResults[i] =
                $"Question {i + 1}. {this.TranslationTable[index, 1]} = {this.TranslationTable[index, 0]} - Correct";
        else
            this.TrainingResults[i] =
                $"Question {i + 1}. {this.TranslationTable[index, 1]} = {this.TranslationTable[index, 0]} - Wrong - Your answer was {answer}";
    }
}