// Modules/AdminModule.cs

// This module is for Admin users only.
// Admin can view all employees, add new employees, and create user accounts.

using PayrollSystem.Models; // we need this to use User and Employee classes
using PayrollSystem.Services; // we need this to use FileHandler

namespace PayrollSystem.Modules
{
    public class AdminModule
    {
        private readonly List<User> _users;         // list of all system users
        private readonly List<Employee> _employees; // list of all employees
        private readonly List<EmployeeHistory> _employeeHistories; // list of all employee histories
        private readonly FileHandler _fileHandler;  // to handle file operations

        public AdminModule(List<User> users, List<Employee> employees, List<EmployeeHistory> employeeHistories)
        {
            _users = users;
            _employees = employees;
            _employeeHistories = employeeHistories;
            _fileHandler = new FileHandler();
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("+=============================================+");
                Console.WriteLine("|               ADMIN MAIN MENU               |");
                Console.WriteLine("+=============================================+");
                Console.ResetColor();

                Console.WriteLine("[1] View Employees");
                Console.WriteLine("[2] Add Employee");
                Console.WriteLine("[3] Remove Employee");
                Console.WriteLine("[4] Manage Users");
                Console.WriteLine("[5] View Employee History");
                Console.WriteLine("[6] Logout");
                Console.Write("\nSelect an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ViewEmployees(); break;
                    case "2": AddEmployee(); break;
                    case "3": RemoveEmployee(); break;
                    case "4": ManageUsers(); break;
                    case "5": ViewEmployeeHistory(); break;
                    case "6":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n[!] Logging out...");
                        Console.ResetColor();
                        Thread.Sleep(1000); // 1 second delay
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

            Console.Write("Annual Salary ($): ");
            if (!double.TryParse(Console.ReadLine(), out double salary))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[!] Invalid salary input. Press Enter to return.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            var newEmployee = new Employee(name, position, department, salary);
            _employees.Add(newEmployee);

            // Add initial history record
            _employeeHistories.Add(new EmployeeHistory(name, "New Employee",
                $"Started as {position} in {department} with salary ${salary}"));

            // Save both employees and histories to files
            _fileHandler.SaveEmployees(_employees);
            _fileHandler.SaveEmployeeHistories(_employeeHistories);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[✔] Employee added successfully. Press Enter to continue.");
            Console.ResetColor();
            Console.ReadLine();
        }

        private void RemoveEmployee()
        {
            Console.Clear();
            Console.WriteLine("=== Remove Employee ===\n");

            Console.Write("Enter employee ID to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int employeeId))
            {
                Console.WriteLine("Invalid employee ID.");
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                return;
            }

            var employee = _employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (employee == null)
            {
                Console.WriteLine("Employee not found.");
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n⚠️  WARNING: DELETING THIS EMPLOYEE WILL PERMANENTLY REMOVE ALL THEIR DATA FROM THE SYSTEM ⚠️");
            Console.WriteLine("\nThe following data will be deleted:");
            Console.WriteLine("1. Employee record");
            Console.WriteLine("2. User account (if exists)");
            Console.WriteLine("3. All attendance records");
            Console.WriteLine("4. All payroll records");
            Console.WriteLine("5. All employee history records");
            Console.WriteLine("\nTHIS ACTION CANNOT BE UNDONE!");
            Console.ResetColor();

            Console.Write($"\nAre you sure you want to remove {employee.Name}? (y/n): ");
            if (Console.ReadLine()?.ToLower() != "y")
            {
                Console.WriteLine("Operation cancelled.");
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                return;
            }

            // Remove employee
            _employees.Remove(employee);

            // Remove user account if exists
            var user = _users.FirstOrDefault(u => u.EmployeeId == employeeId);
            if (user != null)
            {
                _users.Remove(user);
            }

            // Remove employee history
            _employeeHistories.RemoveAll(h => h.EmployeeId == employeeId);

            // Remove leave requests
            var leaveRequests = _fileHandler.LoadLeaveRequests();
            leaveRequests.RemoveAll(l => l.EmployeeName.ToLower() == employee.Name.ToLower());
            _fileHandler.SaveLeaveRequests(leaveRequests);

            // Remove attendance records
            var attendanceRecords = _fileHandler.LoadAttendance();
            attendanceRecords.RemoveAll(a => a.EmployeeName.ToLower() == employee.Name.ToLower());
            _fileHandler.SaveAttendance(attendanceRecords);

            // Remove payroll records
            var payrollRecords = _fileHandler.LoadPayrollRecords();
            payrollRecords.RemoveAll(p => p.EmployeeId == employeeId);
            _fileHandler.SavePayrollRecords(payrollRecords);

            // Save all changes to files
            _fileHandler.SaveEmployees(_employees);
            _fileHandler.SaveUsers(_users);
            _fileHandler.SaveEmployeeHistories(_employeeHistories);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nEmployee and all related data removed successfully.");
            Console.ResetColor();
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }

        private void ManageUsers()
        {
            Console.Clear();
            Console.WriteLine("=== Manage Users ===\n");

            Console.WriteLine("1. Add User");
            Console.WriteLine("2. Remove User");
            Console.WriteLine("3. Return to Admin Menu");
            Console.Write("\nSelect option (1-3): ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid option.");
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                return;
            }

            switch (choice)
            {
                case 1:
                    AddUser();
                    break;
                case 2:
                    RemoveUser();
                    break;
                case 3:
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    Console.WriteLine("\nPress Enter to continue...");
                    Console.ReadLine();
                    break;
            }
        }

        private void AddUser()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("+============== ADD NEW USER ==============+");
            Console.ResetColor();

            // First show available employees
            Console.WriteLine("\nAvailable Employees:");
            Console.WriteLine("-------------------");
            foreach (var emp in _employees)
            {
                Console.WriteLine($"[{emp.EmployeeId:D4}] {emp.Name} - {emp.Position}");
            }
            Console.WriteLine("-------------------");

            // Ask for employee ID
            Console.Write("\nEnter Employee ID (or 0 for admin/HR user): ");
            if (!int.TryParse(Console.ReadLine(), out int employeeId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[!] Invalid employee ID format.");
                Console.ResetColor();
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                return;
            }

            // Validate employee ID
            if (employeeId != 0)
            {
                var employee = _employees.FirstOrDefault(e => e.EmployeeId == employeeId);
                if (employee == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n[!] Employee not found.");
                    Console.ResetColor();
                    Console.WriteLine("\nPress Enter to continue...");
                    Console.ReadLine();
                    return;
                }

                // Check if employee already has a user account
                if (_users.Any(u => u.EmployeeId == employeeId))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n[!] This employee already has a user account.");
                    Console.ResetColor();
                    Console.WriteLine("\nPress Enter to continue...");
                    Console.ReadLine();
                    return;
                }
            }

            Console.Write("\nEnter username: ");
            string? username = Console.ReadLine();
            Console.Write("Enter password: ");
            string? password = Console.ReadLine();

            // Determine role based on employeeId
            string role;
            if (employeeId == 0)
            {
                Console.Write("Enter role (admin/hr): ");
                role = Console.ReadLine()?.ToLower() ?? "";
                if (role != "admin" && role != "hr")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n[!] Invalid role. Must be 'admin' or 'hr'.");
                    Console.ResetColor();
                    Console.WriteLine("\nPress Enter to continue...");
                    Console.ReadLine();
                    return;
                }
            }
            else
            {
                role = "employee";
            }

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[!] Username and password are required.");
                Console.ResetColor();
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                return;
            }

            if (_users.Any(u => u.Username == username))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[!] Username already exists.");
                Console.ResetColor();
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                return;
            }

            _users.Add(new User(username, password, role, employeeId));
            // Save users to file
            _fileHandler.SaveUsers(_users);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[✔] User added successfully.");
            Console.ResetColor();
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }

        private void RemoveUser()
        {
            Console.Clear();
            Console.WriteLine("=== Remove User ===\n");

            Console.Write("Enter username to remove: ");
            string? username = Console.ReadLine();

            if (string.IsNullOrEmpty(username))
            {
                Console.WriteLine("Username is required.");
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                return;
            }

            var user = _users.FirstOrDefault(u => u.Username == username);
            if (user == null)
            {
                Console.WriteLine("User not found.");
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                return;
            }

            Console.Write($"Are you sure you want to remove user {username}? (y/n): ");
            if (Console.ReadLine()?.ToLower() != "y")
            {
                Console.WriteLine("Operation cancelled.");
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                return;
            }

            _users.Remove(user);

            // Save changes to file
            _fileHandler.SaveUsers(_users);

            Console.WriteLine("User removed successfully.");
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }

        private void ViewEmployeeHistory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("+========= EMPLOYEE HISTORY =========+");
            Console.ResetColor();

            // Show list of employees with their IDs
            Console.WriteLine("\nAvailable Employees:");
            Console.WriteLine("-------------------");
            foreach (var emp in _employees)
            {
                Console.WriteLine($"[{emp.EmployeeId:D4}] {emp.Name} - {emp.Position}");
            }
            Console.WriteLine("-------------------");

            // Ask for the employee ID
            Console.Write("\nEnter Employee ID: ");
            if (!int.TryParse(Console.ReadLine(), out int employeeId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[!] Invalid ID format. Press Enter to return.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            var employee = _employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (employee == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[!] Employee not found. Press Enter to return.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            var history = _employeeHistories.Where(h => h.EmployeeId == employeeId).ToList();
            if (!history.Any())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n[!] No history found for this employee.");
                Console.ResetColor();
                Console.WriteLine("\nPress Enter to return.");
                Console.ReadLine();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nHistory for {employee.Name} (ID: {employeeId:D4}):");
            Console.ResetColor();
            Console.WriteLine("------------------------------------------------");
            foreach (var record in history.OrderByDescending(h => h.Date))
            {
                Console.WriteLine($"Date    : {record.Date:yyyy-MM-dd}");
                Console.WriteLine($"Action  : {record.Action}");
                Console.WriteLine($"Details : {record.Details}");
                Console.WriteLine("------------------------------------------------");
            }

            Console.WriteLine("\nPress Enter to return.");
            Console.ReadLine();
        }
    }
}
