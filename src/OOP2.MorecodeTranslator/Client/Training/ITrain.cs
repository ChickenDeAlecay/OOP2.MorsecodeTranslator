namespace MorsecodeTranslator.Core.Training;

public interface ITrain
{
    public string[,] TranslationTable { get; set; }

    public string[] TrainingResults { get; set; }

    public void Train(string? name);
}