namespace MorsecodeTranslator.Core.UserAccountManagement;

public static class ReadUsersFile
{
    public static List<string> GetUsersInfo(string path)
    {
        var file = File.ReadAllLines(path);

        return file.ToList();
    }
}