namespace Menu;

using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Resources;

public abstract class BaseUserAccount
{
    internal static string HidePassword()
    {
        var userPassword = "";
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

    internal static bool CheckUserExists(string userUsername)
    {
        var usersInfo = ReadUsersFile.GetUsersInfo();

        for (var i = 0; i < usersInfo.Length / 2; i++)
            if (usersInfo[i, 0] == userUsername)
                return true;

        return false;
    }

    internal static bool VerifyPassword(string userUsername, string userPassword)
    {
        var usersInfo = ReadUsersFile.GetUsersInfo();

        byte[] salt = { };
        var hash = string.Empty;

        for (var i = 0; i < usersInfo.Length / 2; i++)
            if (usersInfo[i, 0] == userUsername)
            {
                hash = usersInfo[i, 1];
                salt = Encoding.ASCII.GetBytes(userUsername + userUsername.Length);
            }

        var hashToCompare = BaseUserAccount.HashPassword(userUsername, userPassword);

        return string.Equals(hashToCompare, hash);
    }


    //from: https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-8.0
    internal static string HashPassword(string userUsername, string userPassword)
    {
        // Generate a 128-bit salt using a sequence of
        // cryptographically strong random bytes.
        var salt = Encoding.ASCII.GetBytes(userUsername + userUsername.Length);

        // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
        var hashed = Convert.ToBase64String((byte[])KeyDerivation.Pbkdf2(
            userPassword!,
            salt,
            KeyDerivationPrf.HMACSHA256,
            100000,
            256 / 8));

        return hashed;
    }
}