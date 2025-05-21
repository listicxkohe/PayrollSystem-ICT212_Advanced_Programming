// Modules/LoginModule.cs

// This module is for logging in. It checks username and password and tells us who the user is.
using PayrollSystem.Models; // so we can use the User class
using System.Text; // for password masking

namespace PayrollSystem.Modules
{
    public class LoginModule
    {
        private readonly List<User> _users; // this holds all users in memory (like a list of accounts)

        public LoginModule(List<User> users)
        {
            _users = users; // save the user list so we can use it later to check login
        }

        // This is the function that shows the login screen
        public (string Role, string Username) LoginPrompt()
        {
            Console.Clear();

            // Banner Art for SmartHR Console
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("+---------------------------------------------+");
            Console.WriteLine("|  _____                      _   _   _       |");
            Console.WriteLine("| /  ___|                    | | | | | |      |");
            Console.WriteLine("| \\ `--. _ __ ___   __ _ _ __| |_| |_| |_ __  |");
            Console.WriteLine("|  `--. \\ '_ ` _ \\ / _` | '__| __|  _  | '__| |");
            Console.WriteLine("| /\\__/ / | | | | | (_| | |  | |_| | | | |    |");
            Console.WriteLine("| \\____/|_| |_| |_|\\__,_|_|   \\__\\_| |_|_|    |");
            Console.WriteLine("|                                             |");
            Console.WriteLine("|             SMARTHR CONSOLE LOGIN           |");
            Console.WriteLine("+---------------------------------------------+");

            Console.ResetColor();

            while (true)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Please enter your credentials below:");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("[Username] > ");
                Console.ResetColor();
                string username = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("[Password] > ");
                Console.ResetColor();
                string password = ReadPassword();

                // search for the user manually
                User user = null;
                foreach (User u in _users)
                {
                    if (u.Username == username && u.Password == password)
                    {
                        user = u;
                        break;
                    }
                }

                if (user != null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n+----------------------------------------------+");
                    Console.WriteLine("|   Login successful! Welcome, " + username + "   |");
                    Console.WriteLine("+----------------------------------------------+\n");
                    Console.ResetColor();
                    Thread.Sleep(1000);
                    return (user.Role, username);
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[!] Wrong username or password. Please try again.\n");
                Console.ResetColor();
            }
        }

        // method to mask password input with '*'
        private string ReadPassword()
        {
            StringBuilder input = new StringBuilder();
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                    break;
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (input.Length > 0)
                    {
                        input.Length--;
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    input.Append(key.KeyChar);
                    Console.Write("*");
                }
            }
            Console.WriteLine();
            return input.ToString();
        }
    }
}
