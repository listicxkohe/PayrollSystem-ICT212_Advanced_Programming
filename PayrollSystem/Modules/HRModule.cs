// Modules/HRModule.cs

// this module is what HR staff will use. it's like their own menu system
// here, HR can look at employees, change stuff, check leave, and do payslips

using PayrollSystem.Models; // so we can use Employee, LeaveRequest, and EmployeeHistory classes

namespace PayrollSystem.Modules
{
    public class HRModule
    {
        private readonly List<Employee> _employees;
        private readonly List<LeaveRequest> _leaveRequests;
        private readonly List<EmployeeHistory> _employeeHistories;

        public HRModule(List<Employee> employees, List<LeaveRequest> leaveRequests, List<EmployeeHistory> histories)
        {
            _employees = employees;
            _leaveRequests = leaveRequests;
            _employeeHistories = histories;
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("+=============================================+");
                Console.WriteLine("|                 HR MAIN MENU                |");
                Console.WriteLine("+=============================================+");
                Console.ResetColor();

                Console.WriteLine("[1] View Employees");
                Console.WriteLine("[2] Edit Employee");
                Console.WriteLine("[3] Calculate Payroll");
                Console.WriteLine("[4] Manage Leave Requests");
                Console.WriteLine("[5] View Employee History");
                Console.WriteLine("[6] Logout");
                Console.Write("\nSelect an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ViewEmployees(); break;
                    case "2": EditEmployee(); break;
                    case "3": CalculatePayroll(); break;
                    case "4": ManageLeaveRequests(); break;
                    case "5": ViewEmployeeHistory(); break;
                    case "6": return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n[!] Invalid option. Press Enter to try again.");
                        Console.ResetColor();
                        Console.ReadLine();
                        break;
                }
            }
        }

        private void ViewEmployees()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("+============= EMPLOYEE LIST =============+");
            Console.ResetColor();

            foreach (var emp in _employees)
                Console.WriteLine(emp);

            Console.WriteLine("\nPress Enter to return.");
            Console.ReadLine();
        }

        private void EditEmployee()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("+========== EDIT EMPLOYEE INFO ==========+");
            Console.ResetColor();

            Console.Write("Enter Employee Name: ");
            string name = Console.ReadLine();

            Employee emp = null;
            foreach (var e in _employees)
            {
                if (e.Name.ToLower() == name.ToLower())
                {
                    emp = e;
                    break;
                }
            }

            if (emp == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[!] Employee not found. Press Enter to return.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            Console.Write("New Position: ");
            emp.Position = Console.ReadLine();
            Console.Write("New Department: ");
            emp.Department = Console.ReadLine();
            Console.Write("New Salary: ");

            if (double.TryParse(Console.ReadLine(), out double salary))
            {
                emp.Salary = salary;
                _employeeHistories.Add(new EmployeeHistory(emp.Name, "Update", "Updated employee info"));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n[✔] Employee updated. Press Enter to continue.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[!] Invalid salary input.");
                Console.ResetColor();
            }
            Console.ReadLine();
        }

        private void CalculatePayroll()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("+=========== CALCULATE PAYROLL ===========+");
            Console.ResetColor();

            Console.Write("Enter Employee Name: ");
            string name = Console.ReadLine();

            Employee emp = null;
            foreach (var e in _employees)
            {
                if (e.Name.ToLower() == name.ToLower())
                {
                    emp = e;
                    break;
                }
            }

            if (emp == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[!] Employee not found. Press Enter to return.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            Console.Write("Hours Worked: ");
            double.TryParse(Console.ReadLine(), out double hours);
            Console.Write("Hourly Rate: ");
            double.TryParse(Console.ReadLine(), out double rate);
            Console.Write("Bonus: ");
            double.TryParse(Console.ReadLine(), out double bonus);
            Console.Write("Deductions: ");
            double.TryParse(Console.ReadLine(), out double deductions);

            double gross = hours * rate + bonus;
            double net = gross - deductions;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n----------- PAYSLIP -----------");
            Console.ResetColor();
            Console.WriteLine($"Name     : {emp.Name}");
            Console.WriteLine($"Position : {emp.Position}");
            Console.WriteLine($"Net Pay  : ${net:F2}");
            Console.WriteLine("------------------------------");
            Console.WriteLine("Press Enter to return.");
            Console.ReadLine();
        }

        private void ManageLeaveRequests()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("+========= LEAVE REQUESTS ==========+");
            Console.ResetColor();

            foreach (var req in _leaveRequests)
                Console.WriteLine(req);

            Console.Write("\nEnter Leave ID to process (0 to cancel): ");
            if (!int.TryParse(Console.ReadLine(), out int id) || id == 0)
                return;

            LeaveRequest request = null;
            foreach (var r in _leaveRequests)
            {
                if (r.LeaveId == id)
                {
                    request = r;
                    break;
                }
            }

            if (request == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[!] Leave request not found. Press Enter to return.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            Console.Write("Approve (A) / Reject (R): ");
            string action = Console.ReadLine().ToUpper();
            switch (action)
            {
                case "A":
                    request.Status = "Approved";
                    break;
                case "R":
                    request.Status = "Rejected";
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n[!] Invalid action. Press Enter to return.");
                    Console.ResetColor();
                    Console.ReadLine();
                    return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n[✔] Status updated. Press Enter to return.");
            Console.ResetColor();
            Console.ReadLine();
        }

        private void ViewEmployeeHistory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("+========= EMPLOYEE HISTORY =========+");
            Console.ResetColor();

            Console.Write("Enter Employee Name: ");
            string name = Console.ReadLine();

            Console.WriteLine();
            foreach (var record in _employeeHistories)
            {
                if (record.EmployeeName.ToLower() == name.ToLower())
                {
                    Console.WriteLine(record);
                }
            }

            Console.WriteLine("\nPress Enter to return.");
            Console.ReadLine();
        }
    }
}
