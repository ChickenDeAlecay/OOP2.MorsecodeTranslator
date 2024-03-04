namespace Resources;

public static class ReadUsersFile
{
    public static List<string> GetUsersInfo(string path)
    {
        var file = File.ReadAllLines(path);

        var usersList = new List<string>();
        //var usersInformation = new string[file.Length, 2];

        //var iteration = 0;
        foreach (var lines in file)
        {
            usersList.Add(lines);
            //var temp = lines.Split(",");

            //usersInformation[iteration, 0] = temp[0];
            //usersInformation[iteration, 1] = temp[1];

            //iteration += 1;
        }

        return usersList;
    }
}