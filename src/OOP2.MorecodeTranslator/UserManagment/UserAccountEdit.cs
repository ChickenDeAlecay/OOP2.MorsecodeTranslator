using Resources;
using System.IO;
using System.Text;

namespace Menu;

public class UserAccountEdit : BaseUserAccount
{
    public static void EditUser(string path)
    {
        do
        {
            var usersInfo = ReadUsersFile.GetUsersInfo(path);

            var selectedAccount = DisplayAllAccounts(usersInfo);

            if (selectedAccount == usersInfo.Count+1)
            {
                break;
            }

            var selectedUser = usersInfo[selectedAccount-1].Split(',');

            bool finishedEditing;
            do
            {
                Console.Clear();
                Console.WriteLine("Do you want to change the Username or Password");
                var fieldToEdit = DisplayMenu.CreateMenu(new[] { "Username", "Password", "Exit" });
                
                switch (fieldToEdit)
                {
                    case 1:
                        //ISSUE - WHEN UPDATING USERNAME, CHANGES SALT AND PASSWORD HASH
                        usersInfo[selectedAccount-1] = ChangeUsername(path) + ',' + selectedUser[1];
                        UpdateFile(usersInfo, path);
                        finishedEditing = true;
                        break;
                    case 2:
                        usersInfo[selectedAccount - 1] = selectedUser[0] + ',' + ChangePassword(selectedUser[0], path);
                        UpdateFile(usersInfo, path);
                        finishedEditing = true;
                        break;
                    case 3:
                        finishedEditing = true;
                        break;
                    default:
                        finishedEditing = false;
                        break;
                }
            } while (finishedEditing == false);
        } while (true);

    }

    private static string ChangeUsername(string path)
    {
        Console.Clear();
        string? newUsername;
        do
        {
            Console.WriteLine("Enter the new Username for the account");
            newUsername = Console.ReadLine();

            if (BaseUserAccount.CheckUserExists(newUsername, path))
            {
                Console.Clear();
                Console.WriteLine("\nUsername already exists\n\n");
                continue;
            }

            break;
        } while (true);

        return newUsername;
    }

    private static string ChangePassword(string userUsername, string path)
    {
        Console.Clear();
        string? newPassword;
        do
        {
            Console.Write("Enter the accounts Password: ");
            newPassword = BaseUserAccount.HidePassword();

            Console.Write("\nRepeat the Password: ");
            var repeatedPassword = BaseUserAccount.HidePassword();

            if (newPassword != repeatedPassword)
            {
                Console.Clear();
                Console.WriteLine("\nPasswords do not match\n\n");
                continue;
            }

            newPassword = BaseUserAccount.HashPassword(userUsername, newPassword);

            break;
        } while (true);

        return newPassword;
    }
}