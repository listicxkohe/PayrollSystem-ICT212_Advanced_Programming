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
        public string Position { get; set; }   // what their job title is (like Manager)
        public string Department { get; set; } // what department they work in (like IT)
        public double Salary { get; set; }     // how much they earn
        public int LeaveBalance { get; set; } = 20; // how many days off they have left (default 20)

        // this is used when we make a new employee
        // we give their name, job, department, and salary
        public Employee(string name, string position, string department, double salary)
            : base(name) // we send the name to the Person constructor too
        {
            Position = position;     // set job title
            Department = department; // set team/department
            Salary = salary;         // set salary
        }

        // this makes it easier to print an employee's info nicely
        public override string ToString()
        {
            return $"{Name} - {Position} - {Department} - ${Salary} - Leave Balance: {LeaveBalance} days";
        }
    }
}
