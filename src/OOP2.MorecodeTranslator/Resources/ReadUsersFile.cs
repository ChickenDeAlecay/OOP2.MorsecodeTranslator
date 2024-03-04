namespace Resources;

public static class ReadUsersFile
{
    public static string[,] GetUsersInfo(string path)
    {
        var file = File.ReadAllLines(path);

        var usersInformation = new string[file.Length, 2];

        var iteration = 0;
        foreach (var lines in file)
        {
            var temp = lines.Split(",");

            usersInformation[iteration, 0] = temp[0];
            usersInformation[iteration, 1] = temp[1];

            iteration += 1;
        }

        return usersInformation;
    }
}