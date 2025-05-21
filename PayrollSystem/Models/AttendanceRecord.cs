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

        // when we make a new attendance record, we give it the name, date, and status
        public AttendanceRecord(string employeeName, string date, string status)
        {
            EmployeeName = employeeName; // who
            Date = date;                 // when
            Status = status;             // present or not
        }
    }
}
