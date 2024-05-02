namespace MorsecodeTranslator.Engine.UserAccountManagement;

using MorsecodeTranslator.Engine.Logging;

public abstract class UserAccountDelete : BaseUserAccount
{
    public static void DeleteUser(string path, string? name, ILog log)
    {
        try
        {
            do
            {
                var usersInfo = ReadUsersFile.GetUsersInfo(path);

                var selectedAccount = BaseUserAccount.DisplayAllAccounts(usersInfo);

                if (selectedAccount == usersInfo.Count + 1) break;

                if (selectedAccount > usersInfo.Count + 1) continue;

                UserAccountDelete.RemoveUser(selectedAccount, usersInfo, path);
                log.CreateLog(name,
                    $"{name} has deleted a user.\n{usersInfo[selectedAccount].Split(',')[0]} has been deleted");
            } while (true);
        }
        catch (Exception exception)
        {
            log.CreateLog(name, $"An error occurred while deleting a user: {exception.Message}");
        }
    }

    private static void RemoveUser(int selectedAccount, List<string> accountFile, string path)
    {
        try
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
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}