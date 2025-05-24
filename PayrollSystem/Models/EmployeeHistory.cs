// Models/EmployeeHistory.cs

// this class is used to keep a log of stuff that happened to an employee
// like if they got promoted, had their salary changed, or anything else important

namespace PayrollSystem.Models
{
    public class EmployeeHistory
    {
        public string EmployeeName { get; set; } // the name of the employee this record is about
        public string Action { get; set; }       // what happened (like "Promoted" or "Salary Updated")
        public string Details { get; set; }      // extra notes about the action
        public DateTime Date { get; set; }       // when it happened (this sets to today automatically)
        public int EmployeeId { get; set; } // unique identifier for the employee

        // when we make a new history log, we give it the name, action, and some details
        public EmployeeHistory(string employeeName, string action, string details, int employeeId = -1)
        {
            EmployeeName = employeeName; // who
            Action = action;             // what happened
            Details = details;           // more info
            Date = DateTime.Now;         // set date to now
            EmployeeId = employeeId;
        }

        // this makes the record show up in a nice way when we print it
        public override string ToString()
        {
            return $"{Date:yyyy-MM-dd} | {Action} | {Details}"; // like: 2025-05-20 | Promotion | Promoted to Manager
        }
    }
}
