namespace Train.Contracts;

public interface ITrain
{
    public string TranslationSet { get; set; }

    public string[,] TranslationTable { get; set; }

    public string[] TrainingResults { get; set; }

    public void Train();
}