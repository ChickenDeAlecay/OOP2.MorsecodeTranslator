namespace Menu;

using User;

public class UserAccountLogin : BaseUserAccount
{
    public static User LoginUser(string path)
    {
        do
        {
            Console.Clear();

            Console.Write("Enter your username: ");
            var userUsername = Console.ReadLine();

            Console.Write("Enter your password: ");
            var userPassword = BaseUserAccount.HidePassword();

            if (!BaseUserAccount.CheckUserExists(userUsername, path))
            {
                Console.Clear();
                Console.WriteLine("Username not found");
                Console.WriteLine("Do you want to try again?\n1. Yes\n2. No");
                if (Console.ReadLine() == "2")
                {
                    break;
                }
                continue;
            }

            if (!BaseUserAccount.VerifyPassword(userUsername, userPassword, path))
            {
                Console.Clear();
                Console.WriteLine("Password incorrect");
                Console.WriteLine("Do you want to try again?\n1. Yes\n2. No");
                if (Console.ReadLine() == "2")
                {
                    break;
                }
                continue;
            }

            return new User(userUsername, true);

        } while (true);

        return new User("", false);
    }
}