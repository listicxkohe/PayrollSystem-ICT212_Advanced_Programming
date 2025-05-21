// Services/FileHandler.cs

// This class is used to save and load data to/from files.
// Think of it like the memory card of your program — it keeps data even after you close the app.

using PayrollSystem.Models; // import the data models like Employee, User etc.

namespace PayrollSystem.Services
{
    public class FileHandler
    {
        private readonly string dataFolder = "data"; // this is the folder where we store all our text files

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
                    if (parts.Length == 3)
                        users.Add(new User(parts[0], parts[1], parts[2])); // create a user and add it to the list
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
                users.Select(u => $"{u.Username},{u.Password},{u.Role}"));
        }

        // Loads employees from the file
        public List<Employee> LoadEmployees()
        {
            EnsureDirectory();
            var employees = new List<Employee>();
            string path = Path.Combine(dataFolder, "employees.txt");

            if (File.Exists(path))
            {
                foreach (var line in File.ReadAllLines(path))
                {
                    var parts = line.Split(',');
                    if (parts.Length == 4 && double.TryParse(parts[3], out double salary))
                        employees.Add(new Employee(parts[0], parts[1], parts[2], salary));
                }
            }

            return employees;
        }

        // Saves employees to the file
        public void SaveEmployees(List<Employee> employees)
        {
            EnsureDirectory();
            File.WriteAllLines(Path.Combine(dataFolder, "employees.txt"),
                employees.Select(e => $"{e.Name},{e.Position},{e.Department},{e.Salary}"));
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
                            RequestDate = DateTime.Parse(parts[5])
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
                requests.Select(r => $"{r.LeaveId}|{r.EmployeeName}|{r.DaysRequested}|{r.Reason}|{r.Status}|{r.RequestDate:yyyy-MM-dd}"));
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
                        histories.Add(new EmployeeHistory(parts[0], parts[1], parts[2])
                        {
                            Date = DateTime.Parse(parts[3])
                        });
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
    }
}
