namespace Logger;

public static class CreateLog
{
    public static void Log(string userName, string message)
    {
        var newFile = $"Logs\\{userName}\\{System.DateTime.Now:dd'-'MM'-'yyyy'--'HH'-'mm'-'ss}.txt";

        File.WriteAllText(newFile,message);
    }
}