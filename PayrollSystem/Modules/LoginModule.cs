// Modules/LoginModule.cs

// This module is for logging in. It checks username and password and tells us who the user is.
using PayrollSystem.Models; // so we can use the User class
using PayrollSystem.Services; // for FileHandler
using System.Text; // for password masking
using System.Threading; // for Thread.Sleep

namespace PayrollSystem.Modules
{
    public class LoginModule
    {
        private readonly List<User> _users; // this holds all users in memory (like a list of accounts)
        private readonly FileHandler _fileHandler;
        private User? _currentUser;
        private int _currentEmployeeId;

        public LoginModule(List<User> users, FileHandler fileHandler)
        {
            _users = users; // save the user list so we can use it later to check login
            _fileHandler = fileHandler;
            _currentUser = null;
            _currentEmployeeId = -1;
        }

        public User? CurrentUser => _currentUser;
        public int CurrentEmployeeId => _currentEmployeeId;

        // This is the function that shows the login screen
        public (string Role, string Username) ShowLoginMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("  _____                      _   _   _           ");
                Console.WriteLine(" /  ___|                    | | | | | |          ");
                Console.WriteLine(" \\ `--. _ __ ___   __ _ _ __| |_| |_| |_ __      ");
                Console.WriteLine("  `--. \\ '_ ` _ \\ / _` | '__| __|  _  | '__|     ");
                Console.WriteLine(" /\\__/ / | | | | | (_| | |  | |_| | | | |        ");
                Console.WriteLine(" \\____/|_| |_| |_|\\__,_|_|   \\__\\_| |_|_|        ");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("          SMARTHR CONSOLE LOGIN          ");
                Console.ResetColor();
                Console.WriteLine("+=============================================+");
                Console.WriteLine("|                 LOGIN MENU                  |");
                Console.WriteLine("+=============================================+");
                Console.ResetColor();

                Console.WriteLine("[1] Login");
                Console.WriteLine("[2] Exit Program");
                Console.Write("\nSelect an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        if (Login())
                        {
                            return (_currentUser!.Role, _currentUser.Username);
                        }
                        break;

                    case "2":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n[!] Exiting program...");
                        Console.ResetColor();
                        Thread.Sleep(1000); // 1 second delay
                        Environment.Exit(0);
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n[!] Invalid option.");
                        Console.ResetColor();
                        Thread.Sleep(1500); // 1.5 second delay for error
                        break;
                }
            }
        }

        public bool Login()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("+============== LOGIN ==============+");
            Console.ResetColor();

            Console.Write("\nUsername: ");
            string? username = Console.ReadLine();
            Console.Write("Password: ");
            string? password = ReadPassword();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[!] Username and password are required.");
                Console.ResetColor();
                Thread.Sleep(1000); // 1 second delay
                return false;
            }

            _currentUser = _users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (_currentUser == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[!] Invalid username or password.");
                Console.ResetColor();
                Thread.Sleep(1000); // 1 second delay
                return false;
            }

            _currentEmployeeId = _currentUser.EmployeeId;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n[✔] Welcome, {_currentUser.Username}!");
            Console.ResetColor();
            Thread.Sleep(1000); // 1 second delay
            return true;
        }

        public void Logout()
        {
            _currentUser = null;
            _currentEmployeeId = -1;
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
