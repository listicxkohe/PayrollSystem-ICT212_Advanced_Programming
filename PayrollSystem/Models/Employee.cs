// Models/Employee.cs

// this file is about creating an employee and what details we want to store about them
// every employee is also a person, and has extra info like job, salary, etc.

namespace PayrollSystem.Models
{
    // this is a basic person class, just has a name for now
    public class Person
    {
        public string Name { get; set; } // every person has a name

        // when we create a person, we give their name
        public Person(string name)
        {
            Name = name;
        }
    }

    // now we make Employee which inherits from Person
    public class Employee : Person
    {
        public static int lastEmployeeId = 0; // keeps track of the last used ID

        public int EmployeeId { get; set; }    // unique identifier for each employee
        public string Position { get; set; }   // what their job title is (like Manager)
        public string Department { get; set; }  // which department they work in
        public double Salary { get; set; }     // annual salary
        public int LeaveBalance { get; set; } = 20;  // default 20 days per year
        public int ExpectedMonthlyWorkingDays { get; set; } = 20;  // default 20 days per month
        public double InsuranceRate { get; set; } = 0.05;  // default 5% of salary
        public double TaxRate { get; set; } = 0.20;  // default 20% tax rate
        public bool HasHealthInsurance { get; set; } = false;
        public bool HasDentalInsurance { get; set; } = false;
        public bool HasVisionInsurance { get; set; } = false;
        public bool HasSuperannuation { get; set; } = false;

        // this is used when we make a new employee
        // we give their name, job, department, and salary
        public Employee(string name, string position, string department, double salary)
            : base(name) // we send the name to the Person constructor too
        {
            EmployeeId = ++lastEmployeeId;
            Position = position;     // set job title
            Department = department; // set team/department
            Salary = salary;         // set salary
            LeaveBalance = 20; // Default 20 days per year
            ExpectedMonthlyWorkingDays = 20; // Default 20 days per month
            InsuranceRate = 0.05; // Default 5%
            TaxRate = 0.20; // Default 20%
            HasHealthInsurance = false;
            HasDentalInsurance = false;
            HasVisionInsurance = false;
            HasSuperannuation = false;
        }

        // Add parameterless constructor for loading from file
        public Employee() : base("")
        {
            EmployeeId = ++lastEmployeeId;
            LeaveBalance = 20; // Default leave balance
            ExpectedMonthlyWorkingDays = 20; // Default working days
            InsuranceRate = 0.05; // Default insurance rate
            TaxRate = 0.2; // Default tax rate
        }

        // this is used when loading an employee from file
        // we give all the details including the ID
        public Employee(int id, string name, string position, string department, double salary)
            : base(name)
        {
            EmployeeId = id;
            Position = position;
            Department = department;
            Salary = salary;
            LeaveBalance = 20; // Default 20 days per year
            ExpectedMonthlyWorkingDays = 20; // Default 20 days per month
            InsuranceRate = 0.05; // Default 5%
            TaxRate = 0.20; // Default 20%
            HasHealthInsurance = false;
            HasDentalInsurance = false;
            HasVisionInsurance = false;
            HasSuperannuation = false;
            // Update the last used ID if needed
            if (id > lastEmployeeId)
                lastEmployeeId = id;
        }

        // this generates a unique ID for each new employee
        private static int GenerateEmployeeId()
        {
            lastEmployeeId++; // increase the number
            return lastEmployeeId; // use it as the new ID
        }

        // this makes it easier to print an employee's info nicely
        public override string ToString()
        {
            return $"ID: {EmployeeId:D4} | {Name} - {Position} - {Department} - ${Salary} - Leave Balance: {LeaveBalance} days";
        }
    }
}
