// Core/PayrollApp.cs

// This is the main controller for the app. It handles loading data, showing menus, and saving everything back.
// Think of it like the boss of the system, telling the modules what to do.

using PayrollSystem.Modules;    // Import menus for login, admin, HR, and employee
using PayrollSystem.Services;   // Import the file loader/saver
using PayrollSystem.Models;     // Import user, employee, etc.
using System.Collections.Generic;

namespace PayrollSystem.Core
{
    public class PayrollApp
    {
        // We use FileHandler to load/save from text files
        private readonly FileHandler _fileHandler = new FileHandler();

        // These are lists that hold all the data in memory while the app is running
        private List<User> Users;
        private List<Employee> Employees;
        private List<AttendanceRecord> Attendance;
        private List<LeaveRequest> LeaveRequests;
        private List<EmployeeHistory> EmployeeHistories;

        // This method starts the whole app
        public void Start()
        {
            LoadData(); // Get data from files

            // Ask the user to log in (returns their role and username)
            var login = new LoginModule(Users);
            var result = login.LoginPrompt(); // returns a tuple (Role, Username)
            string role = result.Role;
            string username = result.Username;

            // Depending on what kind of user logged in, show them the correct menu
            switch (role.ToLower())
            {
                case "admin":
                    new AdminModule(Users, Employees).ShowMenu();
                    break;

                case "hr":
                    new HRModule(Employees, LeaveRequests, EmployeeHistories).ShowMenu();
                    break;

                case "employee":
                    new EmployeeModule(username, Employees, LeaveRequests, Attendance, EmployeeHistories).ShowMenu();
                    break;

                default:
                    Console.WriteLine("Invalid role. Exiting...");
                    break;
            }

            SaveData(); // Save everything back to files
        }

        // This loads all the data from text files when the program starts
        private void LoadData()
        {
            Users = _fileHandler.LoadUsers();
            Employees = _fileHandler.LoadEmployees();
            Attendance = _fileHandler.LoadAttendance();
            LeaveRequests = _fileHandler.LoadLeaveRequests();
            EmployeeHistories = _fileHandler.LoadEmployeeHistories();
        }

        // This saves all the data to files when the program ends
        private void SaveData()
        {
            _fileHandler.SaveUsers(Users);
            _fileHandler.SaveEmployees(Employees);
            _fileHandler.SaveAttendance(Attendance);
            _fileHandler.SaveLeaveRequests(LeaveRequests);
            _fileHandler.SaveEmployeeHistories(EmployeeHistories);
        }
    }
}
