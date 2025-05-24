// Services/FileHandler.cs

// This class is used to save and load data to/from files.
// Think of it like the memory card of your program — it keeps data even after you close the app.

using PayrollSystem.Models; // import the data models like Employee, User etc.

namespace PayrollSystem.Services
{
    public class FileHandler
    {
        private readonly string dataFolder = "data"; // this is the folder where we store all our text files
        private List<Employee> _employees; // to store employees for history lookup
        private List<User> _users;
        private List<AttendanceRecord> _attendance;
        private List<LeaveRequest> _leaveRequests;
        private List<EmployeeHistory> _employeeHistories;
        private List<PayrollRecord> _payrollRecords;

        public FileHandler()
        {
            _employees = new List<Employee>();
            _users = new List<User>();
            _attendance = new List<AttendanceRecord>();
            _leaveRequests = new List<LeaveRequest>();
            _employeeHistories = new List<EmployeeHistory>();
            _payrollRecords = new List<PayrollRecord>();
        }

        // This makes sure the folder exists before we try saving or loading files
        private void EnsureDirectory()
        {
            if (!Directory.Exists(dataFolder))
                Directory.CreateDirectory(dataFolder); // if it's missing, we create it
        }

        // Loads all users from the text file
        public List<User> LoadUsers()
        {
            EnsureDirectory();
            var users = new List<User>();
            string path = Path.Combine(dataFolder, "users.txt");

            // Check if the file exists first
            if (File.Exists(path))
            {
                foreach (var line in File.ReadAllLines(path)) // read line by line
                {
                    var parts = line.Split(','); // split using comma
                    if (parts.Length >= 3)
                    {
                        int employeeId = parts.Length > 3 ? int.Parse(parts[3]) : -1;
                        users.Add(new User(parts[0], parts[1], parts[2], employeeId)); // create a user and add it to the list
                    }
                }
            }

            // If there are no users (first time setup), add a default admin
            if (!users.Any())
            {
                users.Add(new User("admin", "admin", "Admin"));
                SaveUsers(users); // save it back to the file
            }

            return users; // return the full list
        }

        // Saves all users to the file
        public void SaveUsers(List<User> users)
        {
            EnsureDirectory();
            File.WriteAllLines(Path.Combine(dataFolder, "users.txt"),
                users.Select(u => $"{u.Username},{u.Password},{u.Role},{u.EmployeeId}"));
        }

        // Loads employees from the file
        public List<Employee> LoadEmployees()
        {
            _employees = new List<Employee>();
            string filePath = Path.Combine(dataFolder, "employees.txt");

            if (!File.Exists(filePath))
            {
                return _employees;
            }

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length >= 13)
                    {
                        var employee = new Employee
                        {
                            EmployeeId = int.Parse(parts[0]),
                            Name = parts[1],
                            Position = parts[2],
                            Department = parts[3],
                            Salary = double.Parse(parts[4]),
                            LeaveBalance = int.Parse(parts[5]),
                            ExpectedMonthlyWorkingDays = int.Parse(parts[6]),
                            InsuranceRate = double.Parse(parts[7]),
                            TaxRate = double.Parse(parts[8])
                        };

                        employee.HasHealthInsurance = parts[9].Trim().Equals("true", StringComparison.OrdinalIgnoreCase);
                        employee.HasDentalInsurance = parts[10].Trim().Equals("true", StringComparison.OrdinalIgnoreCase);
                        employee.HasVisionInsurance = parts[11].Trim().Equals("true", StringComparison.OrdinalIgnoreCase);
                        employee.HasSuperannuation = parts[12].Trim().Equals("true", StringComparison.OrdinalIgnoreCase);

                        _employees.Add(employee);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading employees: {ex.Message}");
            }

            return _employees;
        }

        // Saves employees to the file
        public void SaveEmployees(List<Employee> employees)
        {
            string filePath = Path.Combine(dataFolder, "employees.txt");

            try
            {
                var lines = new List<string>();
                foreach (var emp in employees)
                {
                    string line = $"{emp.EmployeeId},{emp.Name},{emp.Position},{emp.Department}," +
                                $"{emp.Salary},{emp.LeaveBalance},{emp.ExpectedMonthlyWorkingDays}," +
                                $"{emp.InsuranceRate},{emp.TaxRate}," +
                                $"{emp.HasHealthInsurance.ToString().ToLower()}," +
                                $"{emp.HasDentalInsurance.ToString().ToLower()}," +
                                $"{emp.HasVisionInsurance.ToString().ToLower()}," +
                                $"{emp.HasSuperannuation.ToString().ToLower()}";

                    lines.Add(line);
                }

                File.WriteAllLines(filePath, lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving employees: {ex.Message}");
            }
        }

        // Loads attendance records (check-ins)
        public List<AttendanceRecord> LoadAttendance()
        {
            EnsureDirectory();
            var attendance = new List<AttendanceRecord>();
            string path = Path.Combine(dataFolder, "attendance.txt");

            if (File.Exists(path))
            {
                foreach (var line in File.ReadAllLines(path))
                {
                    var parts = line.Split('|');
                    if (parts.Length == 3)
                        attendance.Add(new AttendanceRecord(parts[0], parts[1], parts[2]));
                }
            }

            return attendance;
        }

        // Saves attendance to the file
        public void SaveAttendance(List<AttendanceRecord> records)
        {
            EnsureDirectory();
            File.WriteAllLines(Path.Combine(dataFolder, "attendance.txt"),
                records.Select(a => $"{a.EmployeeName}|{a.Date}|{a.Status}"));
        }

        // Loads attendance records (alias for LoadAttendance for backward compatibility)
        public List<AttendanceRecord> LoadAttendanceRecords()
        {
            return LoadAttendance();
        }

        // Loads leave requests from the file
        public List<LeaveRequest> LoadLeaveRequests()
        {
            EnsureDirectory();
            var requests = new List<LeaveRequest>();
            string path = Path.Combine(dataFolder, "leaves.txt");

            if (File.Exists(path))
            {
                foreach (var line in File.ReadAllLines(path))
                {
                    var parts = line.Split('|');
                    if (parts.Length == 6)
                    {
                        var req = new LeaveRequest(parts[1], int.Parse(parts[2]), parts[3], parts[4])
                        {
                            LeaveId = int.Parse(parts[0]),
                            Date = parts[5]
                        };
                        // make sure we don't use duplicate IDs later
                        if (req.LeaveId > LeaveRequest.lastLeaveId)
                            LeaveRequest.lastLeaveId = req.LeaveId;
                        requests.Add(req);
                    }
                }
            }

            return requests;
        }

        // Saves leave requests to the file
        public void SaveLeaveRequests(List<LeaveRequest> requests)
        {
            EnsureDirectory();
            File.WriteAllLines(Path.Combine(dataFolder, "leaves.txt"),
                requests.Select(r => $"{r.LeaveId}|{r.EmployeeName}|{r.DaysRequested}|{r.Reason}|{r.Status}|{r.Date}"));
        }

        // Loads history of what happened to each employee (e.g., promotion)
        public List<EmployeeHistory> LoadEmployeeHistories()
        {
            EnsureDirectory();
            var histories = new List<EmployeeHistory>();
            string path = Path.Combine(dataFolder, "employee_histories.txt");

            if (File.Exists(path))
            {
                foreach (var line in File.ReadAllLines(path))
                {
                    var parts = line.Split('|');
                    if (parts.Length == 4)
                    {
                        var history = new EmployeeHistory(parts[0], parts[1], parts[2])
                        {
                            Date = DateTime.Parse(parts[3])
                        };

                        // Find the employee ID by name
                        var employee = _employees.FirstOrDefault(e => e.Name == history.EmployeeName);
                        if (employee != null)
                        {
                            history.EmployeeId = employee.EmployeeId;
                        }

                        histories.Add(history);
                    }
                }
            }

            return histories;
        }

        // Saves the employee histories to file
        public void SaveEmployeeHistories(List<EmployeeHistory> histories)
        {
            EnsureDirectory();
            File.WriteAllLines(Path.Combine(dataFolder, "employee_histories.txt"),
                histories.Select(h => $"{h.EmployeeName}|{h.Action}|{h.Details}|{h.Date:yyyy-MM-dd}"));
        }

        public void SavePayrollRecord(PayrollRecord payroll)
        {
            string payrollPath = Path.Combine(dataFolder, "payrolls.txt");
            string record = $"{payroll.EmployeeId},{payroll.Month},{payroll.DaysWorked},{payroll.DaysOnLeave}," +
                          $"{payroll.BaseSalary},{payroll.Bonus},{payroll.InsuranceDeduction}," +
                          $"{payroll.TaxDeduction},{payroll.NetPay},{payroll.Superannuation}";

            File.AppendAllText(payrollPath, record + Environment.NewLine);
        }

        public List<PayrollRecord> LoadPayrollRecords()
        {
            string payrollPath = Path.Combine(dataFolder, "payrolls.txt");
            var records = new List<PayrollRecord>();

            if (File.Exists(payrollPath))
            {
                foreach (string line in File.ReadAllLines(payrollPath))
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 10)
                    {
                        records.Add(new PayrollRecord(
                            int.Parse(parts[0]),  // EmployeeId
                            parts[1],            // Month
                            int.Parse(parts[2]), // DaysWorked
                            int.Parse(parts[3]), // DaysOnLeave
                            double.Parse(parts[4]), // BaseSalary
                            double.Parse(parts[5]), // Bonus
                            double.Parse(parts[6]), // InsuranceDeduction
                            double.Parse(parts[7]), // TaxDeduction
                            double.Parse(parts[8]), // NetPay
                            double.Parse(parts[9])  // Superannuation
                        ));
                    }
                }
            }

            return records;
        }

        public void SaveAttendanceRecords(List<AttendanceRecord> records)
        {
            EnsureDirectory();
            File.WriteAllLines(Path.Combine(dataFolder, "attendance.txt"),
                records.Select(a => $"{a.EmployeeName}|{a.Date}|{a.Status}"));
        }

        public void SavePayrollRecords(List<PayrollRecord> records)
        {
            var lines = records.Select(p => $"{p.EmployeeId},{p.Month},{p.DaysWorked},{p.DaysOnLeave},{p.BaseSalary},{p.Bonus},{p.InsuranceDeduction},{p.TaxDeduction},{p.NetPay},{p.Superannuation}");
            File.WriteAllLines(Path.Combine(dataFolder, "payrolls.txt"), lines);
        }
    }
}
