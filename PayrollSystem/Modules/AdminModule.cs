// Modules/AdminModule.cs

// This module is for Admin users only.
// Admin can view all employees, add new employees, and create user accounts.

using PayrollSystem.Models; // we need this to use User and Employee classes

namespace PayrollSystem.Modules
{
    public class AdminModule
    {
        private readonly List<User> _users;         // list of all system users
        private readonly List<Employee> _employees; // list of all employees

        public AdminModule(List<User> users, List<Employee> employees)
        {
            _users = users;
            _employees = employees;
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("+===============================================+");
                Console.WriteLine("|                  ADMIN MENU                   |");
                Console.WriteLine("+===============================================+");
                Console.ResetColor();

                Console.WriteLine("[1] View Employees");
                Console.WriteLine("[2] Add Employee");
                Console.WriteLine("[3] Add User");
                Console.WriteLine("[4] Logout");
                Console.Write("\nSelect an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewEmployees();
                        break;
                    case "2":
                        AddEmployee();
                        break;
                    case "3":
                        AddUser();
                        break;
                    case "4":
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n[!] Invalid option. Press Enter to try again.");
                        Console.ResetColor();
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void ViewEmployees()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("+==================== EMPLOYEES ====================+");
            Console.ResetColor();

            foreach (var emp in _employees)
                Console.WriteLine(emp);

            Console.WriteLine("\nPress Enter to return to the menu.");
            Console.ReadLine();
        }

        private void AddEmployee()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("+============== ADD NEW EMPLOYEE ==============+");
            Console.ResetColor();

            Console.Write("Full Name         : ");
            string name = Console.ReadLine();
            Console.Write("Position          : ");
            string position = Console.ReadLine();
            Console.Write("Department        : ");
            string department = Console.ReadLine();
            Console.Write("Monthly Salary ($): ");

            if (!double.TryParse(Console.ReadLine(), out double salary))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[!] Invalid salary input. Press Enter to return.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            _employees.Add(new Employee(name, position, department, salary));

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[✔] Employee added successfully. Press Enter to continue.");
            Console.ResetColor();
            Console.ReadLine();
        }

        private void AddUser()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("+================= ADD NEW USER =================+");
            Console.ResetColor();

            Console.Write("Username         : ");
            string username = Console.ReadLine();
            Console.Write("Password         : ");
            string password = Console.ReadLine();
            Console.Write("Role (Admin/HR/Employee): ");
            string role = Console.ReadLine();

            _users.Add(new User(username, password, role));

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[✔] User account created successfully. Press Enter to continue.");
            Console.ResetColor();
            Console.ReadLine();
        }
    }
}
