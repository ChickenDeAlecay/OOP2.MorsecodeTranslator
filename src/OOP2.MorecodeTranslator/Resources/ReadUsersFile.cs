namespace Resources;

public static class ReadUsersFile
{
    public static List<string> GetUsersInfo(string path)
    {
        var file = File.ReadAllLines(path);

        var usersList = new List<string>();

        foreach (var lines in file)
        {
            usersList.Add(lines);
        }

        return usersList;
    }
}