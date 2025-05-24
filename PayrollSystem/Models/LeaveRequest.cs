// Models/LeaveRequest.cs

// this class is used when an employee asks for leave (day(s) off)
// it keeps track of who asked, how many days, why, and whether it's approved or not

namespace PayrollSystem.Models
{
    public class LeaveRequest
    {
        public static int lastLeaveId = 0; // this keeps count of the last used ID so every leave request has a unique ID

        public int LeaveId { get; set; }            // unique number to identify each leave request
        public string EmployeeName { get; set; }    // who is asking for leave
        public int DaysRequested { get; set; }      // how many days they want off
        public string Reason { get; set; }          // reason for asking leave
        public string Status { get; set; }          // is it Approved, Rejected or still Pending?
        public string Date { get; set; }  // Format: "YYYY-MM-DD"
        public int EmployeeId { get; set; } // unique identifier for the employee

        // this is called when someone makes a new leave request
        public LeaveRequest(string employeeName, int daysRequested, string reason, string status, int employeeId = -1)
        {
            LeaveId = ++lastLeaveId;
            EmployeeName = employeeName;
            DaysRequested = daysRequested;
            Reason = reason;
            Status = status;
            Date = DateTime.Now.ToString("yyyy-MM-dd");
            EmployeeId = employeeId;
        }

        // this makes it easy to print a leave request in a nice readable format
        public override string ToString()
        {
            return $"Leave ID: {LeaveId:D4} | {EmployeeName} - {DaysRequested} days - {Status}\n" +
                   $"Reason: {Reason}\n" +
                   $"Date: {Date}";
        }
    }
}
