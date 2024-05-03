namespace MorsecodeTranslator.Engine.Logging;

using MorsecodeTranslator.Engine.UserConsoleInput;

public class Log : ILog
{
    public string CreateLog(string? userName, string message, string setId)
    {
        var newFile = $"Logs\\{userName}\\{setId}-{DateTime.Now:dd'-'MM'-'yyyy'--'HH'-'mm'-'ss}.txt";

        File.WriteAllText(newFile, message);

        return newFile;
    }

    public string CreateLog(string? userName, string message)
    {
        var newFile = $"Logs\\{userName}\\{DateTime.Now:dd'-'MM'-'yyyy'--'HH'-'mm'-'ss}.txt";

        File.WriteAllText(newFile, message);

        return newFile;
    }

    public string ReadLog(string setId)
    {
        do
        {
            Console.Clear();

            var directories = Directory.GetDirectories("Logs")
                                       .Where(dir => !dir.EndsWith("admin"))
                                       .ToArray();

            var selectedDirectory = DisplayMenu.CreateMenu(directories);

            if (selectedDirectory == directories.Length + 1) break;
            if (selectedDirectory > directories.Length + 1) continue;

            Console.Clear();

            var files = Directory.GetFiles(directories[selectedDirectory - 1])
                                 .Where(file => Path.GetFileName(file)?.StartsWith(setId[0].ToString()) == true)
                                 .ToArray();

            var selectedFile = DisplayMenu.CreateMenu(files);

            if (selectedFile == files.Length + 1) break;
            if (selectedFile > files.Length + 1) continue;

            var message = File.ReadAllText(files[selectedFile - 1]);
            return message;
        } while (true);

        return string.Empty;
    }
}