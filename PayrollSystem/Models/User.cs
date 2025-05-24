// Models/User.cs

// this is a simple blueprint for a user account
// every user has a username, password, and role (like admin, HR, employee)

namespace PayrollSystem.Models
{
    public class User
    {
        // the name they use to login
        public string Username { get; set; }

        // the password for their account (not encrypted in this example)
        public string Password { get; set; }

        // their role: can be "Admin", "HR", or "Employee"
        public string Role { get; set; }

        // Employee ID
        public int EmployeeId { get; set; } // -1 means no employee associated

        // when we create a new user, we give their name, password, and role
        public User(string username, string password, string role, int employeeId = -1)
        {
            Username = username; // save username
            Password = password; // save password
            Role = role;         // save what kind of user they are
            EmployeeId = employeeId;
        }
    }
}
