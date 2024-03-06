namespace Menu;

using User;

public class UserAccountLogin : BaseUserAccount
{
    public static User LoginUser(string path)
    {
        Console.Clear();
        do
        {
            Console.Write("Enter your username: ");
            var userUsername = Console.ReadLine();

            Console.Write("Enter your password: ");
            var userPassword = BaseUserAccount.HidePassword();

            if (!BaseUserAccount.CheckUserExists(userUsername, path))
            {
                Console.Clear();
                Console.WriteLine("Username not found");
                continue;
            }

            if (!BaseUserAccount.VerifyPassword(userUsername, userPassword, path))
            {
                Console.Clear();
                Console.WriteLine("Password incorrect");
                continue;
            }

            return new User(userUsername);

        } while (true);
    }
}