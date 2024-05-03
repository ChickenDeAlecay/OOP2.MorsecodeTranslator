namespace MorsecodeTranslator.Engine.UserAccountManagement;

using System.Security.Cryptography;
using System.Text;
using MorsecodeTranslator.Engine.Logging;

public abstract class UserAccountRegister : BaseUserAccount
{
    public static void RegisterUser(string path, string? name, ILog log)
    {
        Console.Clear();

        var userUsername = BaseUserAccount.GetUserName();

        var userPassword = UserAccountRegister.GetUserPassword();

        var rndNumSalt = RandomNumberGenerator.GetInt32(0, 10000);

        userPassword = BaseUserAccount.HashPassword(userPassword, rndNumSalt);
        UserAccountRegister.WriteUserToFile(userUsername, userPassword, rndNumSalt, path);
        Directory.CreateDirectory($"Logs\\{userUsername}");
        log.CreateLog(name,
            $"New user {userUsername} has been created.\nNew directory created at: Logs\\{userUsername}");
    }

    private static void WriteUserToFile(string? userUsername, string userPassword, int rndNumSalt, string path)
    {
        var newUser = new List<string> { userUsername + ',' + userPassword + ',' + rndNumSalt };

        File.AppendAllLines(path, newUser, Encoding.UTF8);
    }

    private static string GetUserPassword()
    {
        do
        {
            Console.Write("Enter the accounts Password: ");
            var userPassword = BaseUserAccount.HidePassword();

            Console.Write("\nRepeat the Password        : ");
            var repeatedPassword = BaseUserAccount.HidePassword();

            if (string.IsNullOrEmpty(userPassword))
            {
                Console.WriteLine("Please enter a password");
                continue;
            }

            if (userPassword == repeatedPassword) return userPassword;

            Console.Clear();
            Console.WriteLine("\nPasswords do not match\n\n");
        } while (true);
    }
}