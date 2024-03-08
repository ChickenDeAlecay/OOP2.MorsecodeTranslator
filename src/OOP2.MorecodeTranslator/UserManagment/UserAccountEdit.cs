namespace Menu;

using Logger;
using Resources;

public class UserAccountEdit : BaseUserAccount
{
    public static void EditUser(string path, string? name)
    {
        do
        {
            var usersInfo = ReadUsersFile.GetUsersInfo(path);

            var selectedAccount = BaseUserAccount.DisplayAllAccounts(usersInfo);

            if (selectedAccount == usersInfo.Count + 1) break;

            string?[] selectedUser = usersInfo[selectedAccount - 1].Split(',');

            bool finishedEditing;
            do
            {
                Console.Clear();
                Console.WriteLine("Do you want to change the Username or Password");
                var fieldToEdit = DisplayMenu.CreateMenu(new[] { "Username", "Password", "Exit" });

                switch (fieldToEdit)
                {
                    case 1:
                        var oldName = usersInfo[0];
                        usersInfo[selectedAccount - 1] = UserAccountEdit.ChangeUsername(path) + ',' + selectedUser[1] +
                                                         ',' + selectedUser[2];
                        BaseUserAccount.UpdateFile(usersInfo, path);
                        CreateLog.Log(name,
                            $"{name} has updated a user.\nUsers name updated to {usersInfo[0]} from {oldName}");
                        finishedEditing = true;
                        break;
                    case 2:
                        usersInfo[selectedAccount - 1] = selectedUser[0] + ',' +
                                                         UserAccountEdit.ChangePassword(int.Parse(selectedUser[2] ?? throw new InvalidOperationException())) + ',' + selectedUser[2];
                        BaseUserAccount.UpdateFile(usersInfo, path);
                        CreateLog.Log(name, $"{name} has updated a user.\n{usersInfo[0]}'s password has been updated");
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

        return newUsername ?? throw new InvalidOperationException();
    }

    private static string ChangePassword(int salt)
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

            newPassword = BaseUserAccount.HashPassword(newPassword, salt);

            break;
        } while (true);

        return newPassword;
    }
}