// Program.cs
// Entry point for the SmartHR Console application.
// This file is responsible for initializing core services, loading data, and starting the main application loop.

using PayrollSystem.Core;      // Contains the main PayrollApp class that controls the application flow.
using PayrollSystem.Services;  // Provides access to FileHandler for data persistence and encryption.
using PayrollSystem.Modules;   // Includes modules for Login, Admin, HR, and Employee functionality.

namespace PayrollSystem
{
    class Program
    {
        // The Main method is the starting point of the application.
        // args: Command-line arguments (not used in this application).
        static void Main(string[] args)
        {
            // Instantiate the FileHandler service.
            // This class manages all file I/O operations (loading/saving users, employees, payroll, etc.)
            // and handles encryption for sensitive data like user passwords.
            var fileHandler = new FileHandler();

            // Load all user accounts from users.txt (with password decryption).
            // Returns a List<User> containing all registered users (Admin, HR, Employee).
            var users = fileHandler.LoadUsers();

            // Load all employee records from employees.txt.
            // Returns a List<Employee> with all employee details (ID, name, position, salary, etc.).
            var employees = fileHandler.LoadEmployees();

            // Load all payroll records from payrolls.txt.
            // Returns a List<PayrollRecord> with historical payroll data for reporting and analytics.
            var payroll = fileHandler.LoadPayrollRecords();

            // Initialize the LoginModule with the loaded users and file handler.
            // The LoginModule handles user authentication and determines the user's role.
            var loginModule = new LoginModule(users, fileHandler);

            // Create the main PayrollApp instance.
            // PayrollApp manages the main application loop, menu navigation, and delegates control
            // to the appropriate module (Admin, HR, Employee) based on the logged-in user's role.
            var app = new PayrollApp();

            // Start the application.
            // This method displays the login screen, processes user input, and runs the main menu system.
            app.Run();

            // Note: The application will continue running until the user chooses to log out or exit.
        }
    }
}
