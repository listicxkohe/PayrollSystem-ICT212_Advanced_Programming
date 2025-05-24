// Modules/EmployeeModule.cs

// this is the menu that normal employees see when they login
// here they can check-in for work, apply for leave, and look at their own records

using PayrollSystem.Models; // we're using Employee, LeaveRequest, AttendanceRecord, etc.
using PayrollSystem.Services; // for FileHandler
using System.Threading;

namespace PayrollSystem.Modules
{
    public class EmployeeModule
    {
        private readonly string _username;
        private readonly int _employeeId;
        private readonly List<Employee> _employees;
        private readonly List<LeaveRequest> _leaveRequests;
        private readonly List<AttendanceRecord> _attendance;
        private readonly List<EmployeeHistory> _employeeHistories;
        private readonly FileHandler _fileHandler;

        public EmployeeModule(string username, int employeeId, List<Employee> employees, List<LeaveRequest> leaveRequests, List<AttendanceRecord> attendance, List<EmployeeHistory> employeeHistories, FileHandler fileHandler)
        {
            _username = username;
            _employeeId = employeeId;
            _employees = employees;
            _leaveRequests = leaveRequests;
            _attendance = attendance;
            _employeeHistories = employeeHistories;
            _fileHandler = fileHandler;
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("+=============================================+");
                Console.WriteLine("|             EMPLOYEE MAIN MENU              |");
                Console.WriteLine("+=============================================+");
                Console.ResetColor();

                Console.WriteLine("[1] View My Information");
                Console.WriteLine("[2] Request Leave");
                Console.WriteLine("[3] View Leave Requests");
                Console.WriteLine("[4] Check In");
                Console.WriteLine("[5] Logout");
                Console.Write("\nSelect an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ViewMyInfo(); break;
                    case "2": RequestLeave(); break;
                    case "3": ViewLeaveRequests(); break;
                    case "4": CheckIn(); break;
                    case "5":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("\n[!] Logging out...");
                        Console.ResetColor();
                        Thread.Sleep(1000); // 1 second delay
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n[!] Invalid option. Press Enter to try again.");
                        Console.ResetColor();
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void ViewMyProfile()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("+============= MY PROFILE =============+");
            Console.ResetColor();

            // Find the employee's information
            Employee emp = null;
            foreach (var e in _employees)
            {
                if (e.Name.ToLower() == _username.ToLower())
                {
                    emp = e;
                    break;
                }
            }

            if (emp == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[!] Error: Could not find your profile information.");
                Console.ResetColor();
                Console.WriteLine("\nPress Enter to return.");
                Console.ReadLine();
                return;
            }

            // Display profile information
            Console.WriteLine($"\nEmployee ID    : {emp.EmployeeId:D4}");
            Console.WriteLine($"Name           : {emp.Name}");
            Console.WriteLine($"Position       : {emp.Position}");
            Console.WriteLine($"Department     : {emp.Department}");
            Console.WriteLine($"Annual Salary  : ${emp.Salary:F2}");
            Console.WriteLine($"Leave Balance  : {emp.LeaveBalance} days");

            // Count approved leave requests
            int approvedLeaves = _leaveRequests.Count(r =>
                r.EmployeeName.ToLower() == _username.ToLower() &&
                r.Status.ToLower() == "approved");
            Console.WriteLine($"Approved Leaves: {approvedLeaves}");

            // Count pending leave requests
            int pendingLeaves = _leaveRequests.Count(r =>
                r.EmployeeName.ToLower() == _username.ToLower() &&
                r.Status.ToLower() == "pending");
            Console.WriteLine($"Pending Leaves : {pendingLeaves}");

            // Count attendance records for the current month
            int attendanceThisMonth = _attendance.Count(a =>
                a.EmployeeName.ToLower() == _username.ToLower() &&
                a.Date.StartsWith(DateTime.Now.ToString("yyyy-MM")));
            Console.WriteLine($"Days Present   : {attendanceThisMonth}");

            Console.WriteLine("\nPress Enter to return.");
            Console.ReadLine();
        }

        private void CheckIn()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("+============ CHECK IN ============+");
            Console.ResetColor();

            // Check if already checked in today
            string today = DateTime.Today.ToString("yyyy-MM-dd");
            if (_attendance.Any(a =>
                a.EmployeeName.ToLower() == _username.ToLower() &&
                a.Date == today))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n[!] You have already checked in today.");
                Console.ResetColor();
                Console.WriteLine("\nPress Enter to return.");
                Console.ReadLine();
                return;
            }

            // Add new attendance record
            _attendance.Add(new AttendanceRecord(_username, today, "Present"));
            _fileHandler.SaveAttendance(_attendance);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[✔] Checked in successfully!");
            Console.WriteLine($"Date: {today}");
            Console.WriteLine($"Time: {DateTime.Now.ToString("HH:mm:ss")}");
            Console.ResetColor();
            Console.WriteLine("\nPress Enter to return.");
            Console.ReadLine();
        }

        private void RequestLeave()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("+============ LEAVE REQUEST FORM ============+");
            Console.ResetColor();

            // Find the employee's ID
            var employee = _employees.FirstOrDefault(e => e.EmployeeId == _employeeId);
            if (employee == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[!] Error: Could not find your employee information.");
                Console.ResetColor();
                Console.WriteLine("\nPress Enter to return.");
                Console.ReadLine();
                return;
            }

            Console.Write("Days Requested: ");
            if (!int.TryParse(Console.ReadLine(), out int days))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[!] Invalid input. Press Enter to return.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            Console.Write("Reason for leave: ");
            string reason = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(reason))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[!] Reason cannot be empty. Press Enter to return.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            var request = new LeaveRequest(_username, days, reason, "Pending", employee.EmployeeId);
            _leaveRequests.Add(request);
            _fileHandler.SaveLeaveRequests(_leaveRequests); // Save immediately after adding

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n[✔] Leave request submitted! ID: {request.LeaveId}. Press Enter to return.");
            Console.ResetColor();
            Console.ReadLine();
        }

        private void ViewLeaveStatus()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("+=========== YOUR LEAVE REQUESTS ===========+");
            Console.ResetColor();

            foreach (var req in _leaveRequests)
            {
                if (req.EmployeeName.ToLower() == _username.ToLower())
                    Console.WriteLine(req);
            }

            Console.WriteLine("\nPress Enter to return.");
            Console.ReadLine();
        }

        private void ViewMyHistory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("+============= YOUR HISTORY ==============+");
            Console.ResetColor();

            foreach (var h in _employeeHistories)
            {
                if (h.EmployeeName.ToLower() == _username.ToLower())
                    Console.WriteLine(h);
            }

            Console.WriteLine("\nPress Enter to return.");
            Console.ReadLine();
        }

        private void ViewMyInfo()
        {
            Console.Clear();
            Console.WriteLine("=== My Information ===\n");

            var employee = _employees.FirstOrDefault(e => e.EmployeeId == _employeeId);
            if (employee == null)
            {
                Console.WriteLine("Employee information not found.");
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                return;
            }

            Console.WriteLine($"Employee ID: {employee.EmployeeId}");
            Console.WriteLine($"Name: {employee.Name}");
            Console.WriteLine($"Position: {employee.Position}");
            Console.WriteLine($"Department: {employee.Department}");
            Console.WriteLine($"Salary: ${employee.Salary:F2}");
            Console.WriteLine($"Leave Balance: {employee.LeaveBalance} days");
            Console.WriteLine("\nBenefits:");
            if (employee.HasHealthInsurance || employee.HasDentalInsurance ||
                employee.HasVisionInsurance || employee.HasSuperannuation)
            {
                if (employee.HasHealthInsurance) Console.WriteLine("- Health Insurance");
                if (employee.HasDentalInsurance) Console.WriteLine("- Dental Insurance");
                if (employee.HasVisionInsurance) Console.WriteLine("- Vision Insurance");
                if (employee.HasSuperannuation) Console.WriteLine("- Superannuation");
            }
            else
            {
                Console.WriteLine("None");
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }

        private void ViewLeaveRequests()
        {
            Console.Clear();
            Console.WriteLine("=== My Leave Requests ===\n");

            var employee = _employees.FirstOrDefault(e => e.EmployeeId == _employeeId);
            if (employee == null)
            {
                Console.WriteLine("Employee information not found.");
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                return;
            }

            var requests = _leaveRequests.Where(r => r.EmployeeName.ToLower() == _username.ToLower()).ToList();
            if (!requests.Any())
            {
                Console.WriteLine("No leave requests found.");
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
                return;
            }

            foreach (var request in requests)
            {
                Console.WriteLine($"Leave ID: {request.LeaveId:D4}");
                Console.WriteLine($"Date: {request.Date}");
                Console.WriteLine($"Days Requested: {request.DaysRequested}");
                Console.WriteLine($"Status: {request.Status}");
                Console.WriteLine($"Reason: {request.Reason}");
                Console.WriteLine("------------------------------------------------");
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }
    }
}
