namespace MorsecodeTranslator.Core;

public class User(string? name, bool created)
{
    public string? Name { get; set; } = name;

    public bool Created { get; set; } = created;
}