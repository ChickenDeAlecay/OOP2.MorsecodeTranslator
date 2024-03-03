namespace Menu;

using System.Text;

public class UserAccountRegister : BaseUserAccount
{
    public static void RegisterUser()
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

            if (BaseUserAccount.CheckUserExists(userUsername))
            {
                Console.Clear();
                Console.WriteLine("\nUsername already exists\n\n");
                continue;
            }

            userPassword = BaseUserAccount.HashPassword(userUsername, userPassword);
            UserAccountRegister.WriteUserToFile(userUsername, userPassword);

            break;
        } while (true);
    }

    private static void WriteUserToFile(string userUsername, string userPassword)
    {
        var newUser = new List<string>();
        newUser.Add(userUsername + ',' + userPassword);

        File.AppendAllLines("Users\\Logins.csv", newUser, Encoding.UTF8);
    }
}