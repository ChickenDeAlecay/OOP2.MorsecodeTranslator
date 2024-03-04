using Resources;

namespace Menu;

public class UserAccountDelete : BaseUserAccount
{
    public static void DeleteUser(string path)
    {
        do
        {
            var usersInfo = ReadUsersFile.GetUsersInfo(path);

            var selectedAccount = DisplayAllAccounts(usersInfo);

            if (selectedAccount == usersInfo.Count / 2 + 1)
            {
                break;
            }

            

        } while (true);

    }
}