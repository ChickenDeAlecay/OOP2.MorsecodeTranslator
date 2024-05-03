namespace MorsecodeTranslator.Engine.Logging;

public interface ILog
{
    public string CreateLog(string? userName, string message, string setId);

    public string CreateLog(string? userName, string message);

    public string ReadLog(string setId);
}