// Models/AttendanceRecord.cs

// this class is used to track when employees check in
// like a little record that says: on this date, this person was present

namespace PayrollSystem.Models
{
    public class AttendanceRecord
    {
        public string EmployeeName { get; set; } // the name of the employee who checked in
        public string Date { get; set; }          // the day they checked in (as text)
        public string Status { get; set; }        // status like "Present", "Absent", etc.
        public int EmployeeId { get; set; }       // the ID of the employee who checked in
        public string CheckInTime { get; set; }   // the time they checked in

        // Constructor for backward compatibility
        public AttendanceRecord(string employeeName, string date, string status)
        {
            EmployeeName = employeeName;
            Date = date;
            Status = status;
            CheckInTime = "09:00"; // Default check-in time
        }

        // New constructor with all fields
        public AttendanceRecord(string employeeName, string date, string status, int employeeId, string checkInTime)
        {
            EmployeeName = employeeName;
            Date = date;
            Status = status;
            EmployeeId = employeeId;
            CheckInTime = checkInTime;
        }
    }
}
