namespace Train.Contracts;

public interface ITrain
{
    public string TranslationSetPath { get; }

    public string[,] TranslationTable { get; set; }

    public string[] TrainingResults { get; set; }

    public void Train();
}