namespace MorsecodeTranslator.Engine.UserAccountManagement;

using System.Security.Cryptography;
using System.Text;

public class UserAccountRegister : BaseUserAccount
{
    public static void RegisterUser(string path, string? name)
    {
        Console.Clear();
        do
        {
            Console.Write("Enter the accounts Username: ");
            var userUsername = Console.ReadLine();

            Console.Write("Enter the accounts Password: ");
            var userPassword = BaseUserAccount.HidePassword();

            Console.Write("\nRepeat the Password        : ");
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

            var rndNumSalt = RandomNumberGenerator.GetInt32(0, 10000);

            userPassword = BaseUserAccount.HashPassword(userPassword, rndNumSalt);
            UserAccountRegister.WriteUserToFile(userUsername, userPassword, rndNumSalt, path);
            Directory.CreateDirectory($"Logs\\{userUsername}");
            CreateLog.Log(name,
                $"New user {userUsername} has been created.\nNew directory created at: Logs\\{userUsername}");
            break;
        } while (true);
    }

    private static void WriteUserToFile(string? userUsername, string userPassword, int rndNumSalt, string path)
    {
        var newUser = new List<string> { userUsername + ',' + userPassword + ',' + rndNumSalt };

        File.AppendAllLines(path, newUser, Encoding.UTF8);
    }
}