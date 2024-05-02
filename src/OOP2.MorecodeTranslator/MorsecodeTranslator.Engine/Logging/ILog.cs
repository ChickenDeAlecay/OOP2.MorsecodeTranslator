namespace MorsecodeTranslator.Engine.Logging;

public interface ILog
{
    public string CreateLog(string? userName, string message);
}