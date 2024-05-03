namespace MorsecodeTranslator.Engine.UserAccountManagement;

using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using MorsecodeTranslator.Engine.UserConsoleInput;

public abstract class BaseUserAccount
{
    internal static string GetUserName()
    {
        do
        {
            Console.Write("Enter the accounts Username: ");
            var userUsername = Console.ReadLine();

            if (string.IsNullOrEmpty(userUsername) || userUsername.Any(char.IsPunctuation))
            {
                Console.WriteLine("Please enter a valid name");
                continue;
            }

            return userUsername;
        } while (true);
    }

    internal static string HidePassword()
    {
        var userPassword = string.Empty;
        ConsoleKey key;

        do
        {
            var keyInfo = Console.ReadKey(true);
            key = keyInfo.Key;

            if (key == ConsoleKey.Backspace && userPassword.Length > 0)
            {
                Console.Write("\b \b");
                userPassword = userPassword.Remove(userPassword.Length - 1, 1);
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                Console.Write("*");
                userPassword += keyInfo.KeyChar;
            }
        } while (key != ConsoleKey.Enter);

        return userPassword;
    }

    internal static bool CheckUserExists(string? userUsername, string path)
    {
        var usersInfo = ReadUsersFile.GetUsersInfo(path);

        foreach (var user in usersInfo)
        {
            var userSplit = user.Split(',');
            if (userSplit[0] == userUsername) return true;
        }

        return false;
    }

    internal static bool VerifyPassword(string? userUsername, string userPassword, string path)
    {
        try
        {
            var usersInfo = ReadUsersFile.GetUsersInfo(path);

            var salt = 0;
            var hash = string.Empty;

            foreach (var users in usersInfo)
            {
                var userSplit = users.Split(",");
                if (userSplit[0] == userUsername)
                {
                    hash = userSplit[1];
                    salt = int.Parse(userSplit[2]);
                }
            }

            var hashToCompare = BaseUserAccount.HashPassword(userPassword, salt);

            return string.Equals(hashToCompare, hash);
        }
        catch (Exception exception)
        {
            Console.WriteLine($"An error occurred: {exception.Message}");
            return false;
        }
    }

    //from: https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-8.0
    internal static string HashPassword(string userPassword, int rndNumSalt)
    {
        try
        {
            // Generate a 128-bit salt using a sequence of
            // cryptographically strong random bytes.
            var salt = BitConverter.GetBytes(rndNumSalt);

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                userPassword,
                salt,
                KeyDerivationPrf.HMACSHA256,
                100000,
                256 / 8));

            return hashed;
        }
        catch (Exception exception)
        {
            Console.WriteLine($"An error occurred: {exception.Message}");
            return string.Empty;
        }
    }

    internal static int DisplayAllAccounts(List<string> usersInfo)
    {
        var userUsernames = new string[usersInfo.Count + 1];

        Console.Clear();

        for (var i = 0; i < usersInfo.Count; i++) userUsernames[i] = usersInfo[i].Split(',')[0];

        return DisplayMenu.CreateMenu(userUsernames);
    }

    internal static void UpdateFile(List<string> updatedFile, string path)
    {
        try
        {
            File.WriteAllLines(path, updatedFile, Encoding.UTF8);
        }
        catch (Exception exception)
        {
            Console.WriteLine($"An error occurred: {exception.Message}");
        }
    }
}