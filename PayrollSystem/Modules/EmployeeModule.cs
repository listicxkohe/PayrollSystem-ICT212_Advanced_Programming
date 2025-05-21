// Modules/EmployeeModule.cs

// this is the menu that normal employees see when they login
// here they can check-in for work, apply for leave, and look at their own records

using PayrollSystem.Models; // we're using Employee, LeaveRequest, AttendanceRecord, etc.

namespace PayrollSystem.Modules
{
    public class EmployeeModule
    {
        private readonly string _username;
        private readonly List<Employee> _employees;
        private readonly List<LeaveRequest> _leaveRequests;
        private readonly List<AttendanceRecord> _attendance;
        private readonly List<EmployeeHistory> _employeeHistories;

        public EmployeeModule(string username, List<Employee> employees, List<LeaveRequest> leaveRequests, List<AttendanceRecord> attendance, List<EmployeeHistory> employeeHistories)
        {
            _username = username;
            _employees = employees;
            _leaveRequests = leaveRequests;
            _attendance = attendance;
            _employeeHistories = employeeHistories;
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.Clear();

                Employee emp = null;
                foreach (var e in _employees)
                {
                    if (e.Name.ToLower() == _username.ToLower())
                    {
                        emp = e;
                        break;
                    }
                }

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("+=============================================+");
                Console.WriteLine("|              EMPLOYEE MAIN MENU             |");
                Console.WriteLine("+=============================================+");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Welcome, {emp?.Name ?? _username}!\n");
                Console.ResetColor();

                Console.WriteLine("[1] Check In");
                Console.WriteLine("[2] Request Leave");
                Console.WriteLine("[3] View Leave Status");
                Console.WriteLine("[4] View My History");
                Console.WriteLine("[5] Logout");
                Console.Write("\nSelect an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": CheckIn(); break;
                    case "2": RequestLeave(); break;
                    case "3": ViewLeaveStatus(); break;
                    case "4": ViewMyHistory(); break;
                    case "5": return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n[!] Invalid choice. Press Enter to try again.");
                        Console.ResetColor();
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void CheckIn()
        {
            _attendance.Add(new AttendanceRecord(_username, DateTime.Today.ToString("yyyy-MM-dd"), "Present"));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[✔] Checked in successfully. Press Enter to return.");
            Console.ResetColor();
            Console.ReadLine();
        }

        private void RequestLeave()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("+============ LEAVE REQUEST FORM ============+");
            Console.ResetColor();

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

            var request = new LeaveRequest(_username, days, reason, "Pending");
            _leaveRequests.Add(request);

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
    }
}
