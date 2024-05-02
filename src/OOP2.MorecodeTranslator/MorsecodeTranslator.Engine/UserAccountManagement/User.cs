namespace MorsecodeTranslator.Engine.UserAccountManagement;

public class User(string? name, bool created)
{
    public string? Name { get; } = name;

    public bool Created { get; } = created;
}