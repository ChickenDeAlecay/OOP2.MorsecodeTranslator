namespace MorsecodeTranslator.Core;

using MorsecodeTranslator.Engine;
using MorsecodeTranslator.Engine.Compression;
using MorsecodeTranslator.Engine.Encryption;
using MorsecodeTranslator.Engine.Logging;
using MorsecodeTranslator.Engine.Training;
using MorsecodeTranslator.Engine.Translation;
using MorsecodeTranslator.Engine.UserAccountManagement;
using MorsecodeTranslator.Engine.UserConsoleInput;

public static class Program
{
    private static void Main()
    {
        Program.InitiateProgram();

        bool exit;
        do
        {
            Console.Clear();

            var menuOption = DisplayMenu.CreateMenu(new[] { "User Login", "Admin Login" });

            User user;

            switch (menuOption)
            {
                case 1:
                    user = UserAccountLogin.LoginUser("Users\\Logins.csv");
                    if (user.Created) Program.SelectMode(user);
                    exit = false;
                    break;
                case 2:
                    user = UserAccountLogin.LoginUser("Users\\Admin.csv");
                    if (user.Created) Program.AdminManagement(user);
                    exit = false;
                    break;
                case 3:
                    exit = true;
                    break;
                default:
                    exit = false;
                    break;
            }
        } while (exit == false);
    }

    private static void AdminManagement(User user)
    {
        bool exit;
        do
        {
            Console.Clear();

            var menuOption = DisplayMenu.CreateMenu(new[]
            {
                "Create New User", "Change User Details", "Delete User Account", "Create New Admin",
                "Change Admin Details",
                "Delete Admin Account"
            });

            var log = new Log();

            switch (menuOption)
            {
                case 1:
                    UserAccountRegister.RegisterUser("Users\\Logins.csv", user.Name, log);
                    exit = false;
                    break;
                case 2:
                    UserAccountEdit.EditUser("Users\\Logins.csv", user.Name, log);
                    exit = false;
                    break;
                case 3:
                    UserAccountDelete.DeleteUser("Users\\Logins.csv", user.Name, log);
                    exit = false;
                    break;
                case 4:
                    UserAccountRegister.RegisterUser("Users\\Admin.csv", user.Name, log);
                    exit = false;
                    break;
                case 5:
                    UserAccountEdit.EditUser("Users\\Admin.csv", user.Name, log);
                    exit = false;
                    break;
                case 6:
                    UserAccountDelete.DeleteUser("Users\\Admin.csv", user.Name, log);
                    exit = false;
                    break;
                case 7:
                    exit = true;
                    break;
                default:
                    exit = false;
                    break;
            }
        } while (exit == false);
    }

    private static void SelectMode(User user)
    {
        var translationSetAmerican = new ReadTranslationSet("Translation Sets\\american.txt");
        var translationSetInternatioanl = new ReadTranslationSet("Translation Sets\\international.txt");
        bool exit;
        do
        {
            Console.Clear();

            var menuOption = DisplayMenu.CreateMenu(new[] { "Translate", "Train" });

            switch (menuOption)
            {
                case 1:
                    Program.Translate(user, translationSetInternatioanl, translationSetAmerican);
                    exit = false;
                    break;
                case 2:
                    Program.Train(user, translationSetInternatioanl, translationSetAmerican);
                    exit = false;
                    break;
                case 3:
                    exit = true;
                    break;
                default:
                    exit = false;
                    break;
            }
        } while (exit == false);
    }

    private static void Translate(User user, ReadTranslationSet translationSetInternational,
        ReadTranslationSet translationSetAmerican)
    {
        bool exit;
        do
        {
            Console.Clear();
            var menuOption = DisplayMenu.CreateMenu(new[]
            {
                "Text to International", "Text to American", "Morsecode to International", "Morsecode to American"
            });
            ITranslate translate;
            string originalMessage, encryptionKey, encryptedMessage, translatedMessage, messageLog;

            var gzip = new Gzip();
            var aesEncryption = new AesEncryption();
            var log = new Log();

            switch (menuOption)
            {
                case 1:
                    originalMessage = GetUserInput.GetUserMessage();
                    encryptionKey = GetUserInput.GetUserKey();

                    translate = new TranslateToMorsecode(translationSetInternational, gzip, aesEncryption);
                    translatedMessage = translate.ProcessData(originalMessage, encryptionKey);

                    messageLog = log.CreateLog(user.Name, translatedMessage);
                    Console.WriteLine("Your message: " + translatedMessage + "\nLog created: " + messageLog);
                    Console.ReadKey();

                    exit = false;
                    break;
                case 2:
                    originalMessage = GetUserInput.GetUserMessage();
                    encryptionKey = GetUserInput.GetUserKey();

                    translate = new TranslateToMorsecode(translationSetAmerican, gzip, aesEncryption);
                    translatedMessage = translate.ProcessData(originalMessage, encryptionKey);

                    messageLog = log.CreateLog(user.Name, translatedMessage);
                    Console.WriteLine("Your message: " + translatedMessage + "\nLog created: " + messageLog);
                    Console.ReadKey();

                    exit = false;
                    break;
                case 3:
                    encryptedMessage = log.ReadLog();
                    encryptionKey = GetUserInput.GetUserKey();

                    translate = new TranslateFromMorsecode(translationSetInternational, gzip, aesEncryption);
                    translatedMessage = translate.ProcessData(encryptedMessage, encryptionKey);

                    Console.WriteLine("Your message: " + translatedMessage);
                    Console.ReadKey();

                    exit = false;
                    break;
                case 4:
                    encryptedMessage = log.ReadLog();
                    encryptionKey = GetUserInput.GetUserKey();

                    translate = new TranslateFromMorsecode(translationSetAmerican, gzip, aesEncryption);
                    translatedMessage = translate.ProcessData(encryptedMessage, encryptionKey);

                    Console.WriteLine("Your message: " + translatedMessage);
                    Console.ReadKey();

                    exit = false;
                    break;
                case 5:
                    exit = true;
                    break;
                default:
                    exit = false;
                    break;
            }
        } while (exit == false);
    }

    private static void Train(User user, ReadTranslationSet translationSetInternatioanl,
        ReadTranslationSet translationSetAmerican)
    {
        bool exit;
        do
        {
            Console.Clear();
            var menuOption = DisplayMenu.CreateMenu(new[] { "International", "American" });

            TrainMorsecode training;

            var log = new Log();

            switch (menuOption)
            {
                case 1:
                    training = new TrainMorsecode(translationSetInternatioanl, log);
                    training.Train(user.Name);
                    exit = false;
                    break;
                case 2:
                    training = new TrainMorsecode(translationSetAmerican, log);
                    training.Train(user.Name);
                    exit = false;
                    break;
                case 3:
                    exit = true;
                    break;
                default:
                    exit = false;
                    break;
            }
        } while (exit == false);
    }

    private static void InitiateProgram()
    {
        if (Directory.Exists("Users") == false)
        {
            Directory.CreateDirectory("Users");
            File.Create("Users\\Logins.csv");
            File.AppendAllLines("Users\\Admin.csv", new List<string>());

            Console.WriteLine("No Admin Accounts found\nPress any key to continue to create acount");
            Console.ReadKey();
            UserAccountRegister.RegisterUser("Users\\Admin.csv", "", new Log());
        }

        if (Directory.Exists("Logs") == false) Directory.CreateDirectory("Logs");

        if (Directory.Exists("Translation Sets") == false)
        {
            Directory.CreateDirectory("Translation Sets");

            var translationsetPath =
                Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.Parent!.FullName,
                    "MorsecodeTranslator.Core\\Translation Sets");

            File.Copy(translationsetPath + "\\american.txt", "Translation Sets\\american.txt");
            File.Copy(translationsetPath + "\\international.txt", "Translation Sets\\international.txt");
        }
    }
}