namespace Menu;

public class UserAccountLogin : BaseUserAccount
{
    public static bool LoginUser()
    {
        Console.Clear();
        do
        {
            Console.Write("Enter your username: ");
            var userUsername = Console.ReadLine();

            Console.Write("Enter your password: ");
            var userPassword = BaseUserAccount.HidePassword();

            if (!BaseUserAccount.CheckUserExists(userUsername))
            {
                Console.Clear();
                Console.WriteLine("Username not found");
                continue;
            }

            if (!BaseUserAccount.VerifyPassword(userUsername, userPassword))
            {
                Console.Clear();
                Console.WriteLine("Password incorrect");
                continue;
            }

            return true;

            //Console.WriteLine(
            //    "\n\nUsername or Password incorrect\n\nPress any key to try again or press escape to go back to the menu: ");

            //var key = Console.ReadKey(true).Key;
            //if (key == ConsoleKey.Escape) return false;
        } while (true);
    }
}