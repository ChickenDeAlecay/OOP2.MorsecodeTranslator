namespace Menu;

using Logger;
using Resources;

public class UserAccountDelete : BaseUserAccount
{
    public static void DeleteUser(string path, string? name)
    {
        do
        {
            var usersInfo = ReadUsersFile.GetUsersInfo(path);

            var selectedAccount = BaseUserAccount.DisplayAllAccounts(usersInfo);

            if (selectedAccount == usersInfo.Count + 1) break;

            if (selectedAccount > usersInfo.Count + 1) continue;

            UserAccountDelete.RemoveUser(selectedAccount, usersInfo, path);
            CreateLog.Log(name,
                $"{name} has deleted a user.\n{usersInfo[selectedAccount].Split(',')[0]} has been deleted");
        } while (true);
    }

    private static void RemoveUser(int selectedAccount, List<string> accountFile, string path)
    {
        Console.WriteLine($"You are about to delete {accountFile[selectedAccount - 1].Split(',')[0]}");
        Console.WriteLine("Do you want to Continue?\n1. Yes\n2. No");
        var confirmation = Console.ReadLine();
        if (confirmation == "1")
        {
            accountFile.RemoveAt(selectedAccount - 1);

            BaseUserAccount.UpdateFile(accountFile, path);
        }
        else if (confirmation == "2") { }
        else
        {
            Console.WriteLine("Invalid Input\nPress any key to try again");
            Console.ReadKey(true);
        }
    }
}