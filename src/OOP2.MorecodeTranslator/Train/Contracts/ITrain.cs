namespace Train.Contracts;

public interface ITrain
{
    public string TranslationSet { get; set; }

    public void Train();
}