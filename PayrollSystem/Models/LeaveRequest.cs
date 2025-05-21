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
        public DateTime RequestDate { get; set; }   // when was the request made

        // this is called when someone makes a new leave request
        public LeaveRequest(string employeeName, int daysRequested, string reason, string status)
        {
            LeaveId = GenerateLeaveId();         // give this request a unique ID
            EmployeeName = employeeName;         // who made the request
            DaysRequested = daysRequested;       // how long
            Reason = reason;                     // why they need time off
            Status = status;                     // default is usually "Pending"
            RequestDate = DateTime.Now;          // set today's date automatically
        }

        // this just increases the ID number each time someone makes a request
        private static int GenerateLeaveId()
        {
            lastLeaveId++; // increase the number
            return lastLeaveId; // use it as the new ID
        }

        // this makes it easy to print a leave request in a nice readable format
        public override string ToString()
        {
            return $"Leave ID: {LeaveId:D4} | Employee: {EmployeeName} | Days: {DaysRequested} | Reason: {Reason} | Status: {Status} | Requested on: {RequestDate:yyyy-MM-dd}";
        }
    }
}
