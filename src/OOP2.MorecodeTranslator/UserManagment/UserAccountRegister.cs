namespace Menu;

using System.Text;

public class UserAccountRegister : BaseUserAccount
{
    public static void RegisterUser(string path)
    {
        Console.Clear();
        do
        {
            Console.Write("Enter the accounts Username :");
            var userUsername = Console.ReadLine();

            Console.Write("Enter the accounts Password: ");
            var userPassword = BaseUserAccount.HidePassword();

            Console.Write("\nRepeat the Password: ");
            var repeatedPassword = BaseUserAccount.HidePassword();

            if (userPassword != repeatedPassword)
            {
                Console.Clear();
                Console.WriteLine("\nPasswords do not match\n\n");
                continue;
            }

            if (BaseUserAccount.CheckUserExists(userUsername, path))
            {
                Console.Clear();
                Console.WriteLine("\nUsername already exists\n\n");
                continue;
            }

            userPassword = BaseUserAccount.HashPassword(userUsername, userPassword);
            UserAccountRegister.WriteUserToFile(userUsername, userPassword, path);

            break;
        } while (true);
    }

    private static void WriteUserToFile(string userUsername, string userPassword, string path)
    {
        var newUser = new List<string> { userUsername + ',' + userPassword };

        File.AppendAllLines(path, newUser, Encoding.UTF8);
    }
}