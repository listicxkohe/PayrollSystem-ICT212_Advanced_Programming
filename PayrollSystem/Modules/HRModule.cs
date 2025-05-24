// Modules/HRModule.cs

// this module is what HR staff will use. it's like their own menu system
// here, HR can look at employees, change stuff, check leave, and do payslips

using PayrollSystem.Models; // so we can use Employee, LeaveRequest, and EmployeeHistory classes
using PayrollSystem.Services;

namespace PayrollSystem.Modules
{
    public class HRModule
    {
        private readonly List<Employee> _employees;
        private readonly List<LeaveRequest> _leaveRequests;
        private readonly List<EmployeeHistory> _employeeHistories;
        private readonly List<AttendanceRecord> _attendance;
        private readonly FileHandler _fileHandler;

        public HRModule(List<Employee> employees, List<LeaveRequest> leaveRequests, List<EmployeeHistory> histories, List<AttendanceRecord> attendance)
        {
            // Initialize with empty lists if null
            _employees = employees ?? new List<Employee>();
            _leaveRequests = leaveRequests ?? new List<LeaveRequest>();
            _employeeHistories = histories ?? new List<EmployeeHistory>();
            _attendance = attendance ?? new List<AttendanceRecord>();
            _fileHandler = new FileHandler();

            // Load employees from file to ensure we have the latest data
            var loadedEmployees = _fileHandler.LoadEmployees();
            if (loadedEmployees.Any())
            {
                // Update the list contents instead of reassigning
                _employees.Clear();
                _employees.AddRange(loadedEmployees);
            }
        }
        // // This method shows the main HR menu and lets them choose what to do
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
                Console.WriteLine("[6] View Payroll History");
                Console.WriteLine("[7] Analytics");
                Console.WriteLine("[8] Logout");
                Console.Write("\nSelect an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ViewEmployees(); break;
                    case "2": EditEmployee(); break;
                    case "3": CalculatePayroll(); break;
                    case "4": ManageLeaveRequests(); break;
                    case "5": ViewEmployeeHistory(); break;
                    case "6": ViewPayrollHistory(); break;
                    case "7": ShowAnalytics(); break;
                    case "8":
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
        // This method shows the analytics menu and lets HR choose what to do
        private void ShowAnalytics()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("+=============================================+");
                Console.WriteLine("|                 ANALYTICS MENU              |");
                Console.WriteLine("+=============================================+");
                Console.ResetColor();

                Console.WriteLine("[1] Generate Payroll Reports");
                Console.WriteLine("[2] Track Salary Growth Trends");
                Console.WriteLine("[3] Return to Main Menu");
                Console.Write("\nSelect an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": GeneratePayrollReports(); break;
                    case "2": TrackSalaryGrowthTrends(); break;
                    case "3": return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n[!] Invalid option. Press Enter to try again.");
                        Console.ResetColor();
                        Console.ReadLine();
                        break;
                }
            }
        }

        // This method manages leave requests, allowing HR to approve or reject them
        private void GeneratePayrollReports()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("+=========== PAYROLL REPORTS ===========+");
            Console.ResetColor();

            // Hardcoded data for payroll reports
            Console.WriteLine("\nDepartment-wise Salary Expenditure and Benefits Distribution:");
            Console.WriteLine("+-------------------+-------------------+-------------------+");
            Console.WriteLine("| Department        | Total Salary ($) | Benefits ($)      |");
            Console.WriteLine("+-------------------+-------------------+-------------------+");
            Console.WriteLine("| IT                | 120,000          | 15,000            |");
            Console.WriteLine("| HR                | 80,000           | 10,000            |");
            Console.WriteLine("| Finance           | 100,000          | 12,000            |");
            Console.WriteLine("| Marketing         | 90,000           | 11,000            |");
            Console.WriteLine("+-------------------+-------------------+-------------------+");

            Console.WriteLine("\nPress Enter to return.");
            Console.ReadLine();
        }

        // This method tracks salary growth trends and displays them
        private void TrackSalaryGrowthTrends()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("+=========== SALARY GROWTH TRENDS ===========+");
            Console.ResetColor();

            // Hardcoded data for salary growth trends
            Console.WriteLine("\nEmployee Salary Growth Trends:");
            Console.WriteLine("+-------------------+-------------------+-------------------+");
            Console.WriteLine("| Employee Name     | 2023 Salary ($)  | 2025 Salary ($)   |");
            Console.WriteLine("+-------------------+-------------------+-------------------+");
            Console.WriteLine("| John Doe          | 50,000           | 60,000            |");
            Console.WriteLine("| Jane Smith        | 55,000           | 65,000            |");
            Console.WriteLine("| Alice Johnson     | 45,000           | 55,000            |");
            Console.WriteLine("| Bob Brown         | 60,000           | 70,000            |");
            Console.WriteLine("+-------------------+-------------------+-------------------+");

            Console.WriteLine("\nDepartment-wise Payroll Analysis:");
            Console.WriteLine("+-------------------+-------------------+-------------------+");
            Console.WriteLine("| Department        | 2023 Payroll ($) | 2025 Payroll ($)  |");
            Console.WriteLine("+-------------------+-------------------+-------------------+");
            Console.WriteLine("| IT                | 500,000          | 600,000           |");
            Console.WriteLine("| HR                | 300,000          | 350,000           |");
            Console.WriteLine("| Finance           | 400,000          | 450,000           |");
            Console.WriteLine("| Marketing         | 350,000          | 400,000           |");
            Console.WriteLine("+-------------------+-------------------+-------------------+");

            Console.WriteLine("\nPress Enter to return.");
            Console.ReadLine();
        }

        // This method manages leave requests, allowing HR to approve or reject them
        private void ViewEmployees()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("+============= EMPLOYEE LIST =============+");
            Console.ResetColor();
            // Show the list of employees
            foreach (var emp in _employees)
            {
                Console.WriteLine($"\n[{emp.EmployeeId:D4}] {emp.Name}");
                Console.WriteLine($"Position  : {emp.Position}");
                Console.WriteLine($"Department: {emp.Department}");
                Console.WriteLine($"Salary    : ${emp.Salary}");
                Console.WriteLine($"Leave Balance: {emp.LeaveBalance} days");

                // Display benefits
                var benefits = new List<string>();
                if (emp.HasHealthInsurance) benefits.Add("Health Insurance");
                if (emp.HasDentalInsurance) benefits.Add("Dental Insurance");
                if (emp.HasVisionInsurance) benefits.Add("Vision Insurance");
                if (emp.HasSuperannuation) benefits.Add("Superannuation");

                Console.WriteLine("Benefits  : " + (benefits.Count > 0 ? string.Join(", ", benefits) : "NONE"));
                Console.WriteLine("----------------------------------------");
            }

            Console.WriteLine("\nPress Enter to return.");
            Console.ReadLine();
        }

        // This method lets HR edit an employee's information
        // It shows current info first, then lets HR choose what to change
        private void EditEmployee()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("+========== EDIT EMPLOYEE INFO ==========+");
            Console.ResetColor();

            // Show list of employees with their IDs
            Console.WriteLine("\nAvailable Employees:");
            Console.WriteLine("-------------------");
            foreach (var emp in _employees)
            {
                Console.WriteLine($"[{emp.EmployeeId:D4}] {emp.Name} - {emp.Position}");
            }
            Console.WriteLine("-------------------");

            // Ask for the employee ID
            Console.Write("\nEnter Employee ID: ");
            if (!int.TryParse(Console.ReadLine(), out int employeeId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[!] Invalid ID format. Press Enter to return.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            // Look for the employee by ID
            Employee selectedEmployee = _employees.FirstOrDefault(e => e.EmployeeId == employeeId);

            // If we can't find the employee, show error and return
            if (selectedEmployee == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[!] Employee not found. Press Enter to return.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            // Main editing loop - keep showing menu until HR chooses to exit
            while (true)
            {
                // Clear screen and show current employee information
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("+========== CURRENT EMPLOYEE INFO ==========+");
                Console.WriteLine($"Name      : {selectedEmployee.Name}");
                Console.WriteLine($"Position  : {selectedEmployee.Position}");
                Console.WriteLine($"Department: {selectedEmployee.Department}");
                Console.WriteLine($"Salary    : ${selectedEmployee.Salary}");
                Console.WriteLine();
                Console.WriteLine("Benefits:");
                var benefits = new List<string>();
                if (selectedEmployee.HasHealthInsurance) benefits.Add("Health Insurance");
                if (selectedEmployee.HasDentalInsurance) benefits.Add("Dental Insurance");
                if (selectedEmployee.HasVisionInsurance) benefits.Add("Vision Insurance");
                if (selectedEmployee.HasSuperannuation) benefits.Add("Superannuation");

                if (benefits.Count == 0)
                    Console.WriteLine("NONE");
                else
                    foreach (var benefit in benefits)
                        Console.WriteLine($"- {benefit}");

                Console.WriteLine();
                Console.WriteLine($"Insurance Base Rate: {selectedEmployee.InsuranceRate:P0}");
                Console.WriteLine("+==========================================+");
                Console.ResetColor();

                Console.WriteLine("\nWhat would you like to edit?");
                Console.WriteLine("1. Position");
                Console.WriteLine("2. Department");
                Console.WriteLine("3. Salary");
                Console.WriteLine("4. Benefits");
                Console.WriteLine("5. Insurance Rate");
                Console.WriteLine("6. Return to previous menu");

                Console.Write("\nSelect option (1-6): ");
                if (int.TryParse(Console.ReadLine(), out int editChoice))
                {
                    if (editChoice == 6)
                        return;

                    switch (editChoice)
                    {
                        case 1: // Position
                            Console.Write("\nEnter new position: ");
                            string newPosition = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(newPosition))
                            {
                                string oldPosition = selectedEmployee.Position;
                                selectedEmployee.Position = newPosition;
                                _employeeHistories.Add(new EmployeeHistory(
                                    selectedEmployee.Name,
                                    "Position Change",
                                    $"Changed from {oldPosition} to {newPosition}",
                                    selectedEmployee.EmployeeId
                                ));
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\n[✔] Position updated successfully.");
                                Console.ResetColor();
                                _fileHandler.SaveEmployees(_employees);
                                _fileHandler.SaveEmployeeHistories(_employeeHistories);
                            }
                            break;

                        case 2: // Department
                            Console.Write("\nEnter new department: ");
                            string newDepartment = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(newDepartment))
                            {
                                string oldDepartment = selectedEmployee.Department;
                                selectedEmployee.Department = newDepartment;
                                _employeeHistories.Add(new EmployeeHistory(
                                    selectedEmployee.Name,
                                    "Department Change",
                                    $"Changed from {oldDepartment} to {newDepartment}",
                                    selectedEmployee.EmployeeId
                                ));
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\n[✔] Department updated successfully.");
                                Console.ResetColor();
                                _fileHandler.SaveEmployees(_employees);
                                _fileHandler.SaveEmployeeHistories(_employeeHistories);
                            }
                            break;

                        case 3: // Salary
                            Console.Write("\nEnter new salary: ");
                            if (double.TryParse(Console.ReadLine(), out double newSalary) && newSalary > 0)
                            {
                                double oldSalary = selectedEmployee.Salary;
                                selectedEmployee.Salary = newSalary;
                                _employeeHistories.Add(new EmployeeHistory(
                                    selectedEmployee.Name,
                                    "Salary Change",
                                    $"Changed from ${oldSalary:F2} to ${newSalary:F2}",
                                    selectedEmployee.EmployeeId
                                ));
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\n[✔] Salary updated successfully.");
                                Console.ResetColor();
                                _fileHandler.SaveEmployees(_employees);
                                _fileHandler.SaveEmployeeHistories(_employeeHistories);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\n[!] Invalid salary amount.");
                                Console.ResetColor();
                            }
                            break;

                        case 4: // Benefits
                            Console.WriteLine("\nAvailable Benefits:");
                            Console.WriteLine("1. Health Insurance");
                            Console.WriteLine("2. Dental Insurance");
                            Console.WriteLine("3. Vision Insurance");
                            Console.WriteLine("4. Superannuation");
                            Console.WriteLine("5. Return to previous menu");

                            Console.Write("\nSelect benefit (1-4) or 5 to return: ");
                            if (int.TryParse(Console.ReadLine(), out int benefitChoice))
                            {
                                if (benefitChoice == 5)
                                    continue;

                                if (benefitChoice >= 1 && benefitChoice <= 4)
                                {
                                    string benefitName = benefitChoice switch
                                    {
                                        1 => "Health Insurance",
                                        2 => "Dental Insurance",
                                        3 => "Vision Insurance",
                                        4 => "Superannuation",
                                        _ => ""
                                    };

                                    bool currentStatus = benefitChoice switch
                                    {
                                        1 => selectedEmployee.HasHealthInsurance,
                                        2 => selectedEmployee.HasDentalInsurance,
                                        3 => selectedEmployee.HasVisionInsurance,
                                        4 => selectedEmployee.HasSuperannuation,
                                        _ => false
                                    };

                                    Console.Write($"\nAdd (A) or Remove (R) {benefitName}? ");
                                    string action = Console.ReadLine().ToUpper();

                                    if (action == "A" && !currentStatus)
                                    {
                                        switch (benefitChoice)
                                        {
                                            case 1:
                                                selectedEmployee.HasHealthInsurance = true;
                                                Console.Write("Enter Insurance Rate (%): ");
                                                if (double.TryParse(Console.ReadLine(), out double healthRate))
                                                {
                                                    selectedEmployee.InsuranceRate = healthRate / 100;
                                                    _employeeHistories.Add(new EmployeeHistory(
                                                        selectedEmployee.Name,
                                                        "Benefit Added",
                                                        $"Added {benefitName} with rate {healthRate}%",
                                                        selectedEmployee.EmployeeId
                                                    ));
                                                }
                                                break;
                                            case 2:
                                                selectedEmployee.HasDentalInsurance = true;
                                                Console.Write("Enter Insurance Rate (%): ");
                                                if (double.TryParse(Console.ReadLine(), out double dentalRate))
                                                {
                                                    selectedEmployee.InsuranceRate = dentalRate / 100;
                                                    _employeeHistories.Add(new EmployeeHistory(
                                                        selectedEmployee.Name,
                                                        "Benefit Added",
                                                        $"Added {benefitName} with rate {dentalRate}%",
                                                        selectedEmployee.EmployeeId
                                                    ));
                                                }
                                                break;
                                            case 3:
                                                selectedEmployee.HasVisionInsurance = true;
                                                Console.Write("Enter Insurance Rate (%): ");
                                                if (double.TryParse(Console.ReadLine(), out double visionRate))
                                                {
                                                    selectedEmployee.InsuranceRate = visionRate / 100;
                                                    _employeeHistories.Add(new EmployeeHistory(
                                                        selectedEmployee.Name,
                                                        "Benefit Added",
                                                        $"Added {benefitName} with rate {visionRate}%",
                                                        selectedEmployee.EmployeeId
                                                    ));
                                                }
                                                break;
                                            case 4:
                                                selectedEmployee.HasSuperannuation = true;
                                                _employeeHistories.Add(new EmployeeHistory(
                                                    selectedEmployee.Name,
                                                    "Benefit Added",
                                                    $"Added {benefitName}",
                                                    selectedEmployee.EmployeeId
                                                ));
                                                break;
                                        }
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine($"\n[✔] {benefitName} has been added successfully.");
                                        Console.ResetColor();

                                        // Save changes immediately
                                        _fileHandler.SaveEmployees(_employees);
                                        _fileHandler.SaveEmployeeHistories(_employeeHistories);
                                    }
                                    else if (action == "R" && currentStatus)
                                    {
                                        switch (benefitChoice)
                                        {
                                            case 1: selectedEmployee.HasHealthInsurance = false; break;
                                            case 2: selectedEmployee.HasDentalInsurance = false; break;
                                            case 3: selectedEmployee.HasVisionInsurance = false; break;
                                            case 4: selectedEmployee.HasSuperannuation = false; break;
                                        }
                                        _employeeHistories.Add(new EmployeeHistory(
                                            selectedEmployee.Name,
                                            "Benefit Removed",
                                            $"Removed {benefitName}",
                                            selectedEmployee.EmployeeId
                                        ));
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine($"\n[✔] {benefitName} has been removed successfully.");
                                        Console.ResetColor();

                                        // Save changes immediately
                                        _fileHandler.SaveEmployees(_employees);
                                        _fileHandler.SaveEmployeeHistories(_employeeHistories);
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        Console.WriteLine($"\n[!] {benefitName} is already {(currentStatus ? "active" : "inactive")}.");
                                        Console.ResetColor();
                                    }
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\n[!] Invalid option.");
                                    Console.ResetColor();
                                }
                            }
                            break;

                        case 5: // Insurance Rate
                            Console.Write($"\nCurrent Insurance Rate: {selectedEmployee.InsuranceRate:P0}");
                            Console.Write("\nEnter new insurance rate (%): ");
                            if (double.TryParse(Console.ReadLine(), out double newRate) && newRate >= 0 && newRate <= 100)
                            {
                                double oldRate = selectedEmployee.InsuranceRate;
                                selectedEmployee.InsuranceRate = newRate / 100;
                                _employeeHistories.Add(new EmployeeHistory(
                                    selectedEmployee.Name,
                                    "Insurance Rate Change",
                                    $"Changed from {oldRate:P0} to {selectedEmployee.InsuranceRate:P0}",
                                    selectedEmployee.EmployeeId
                                ));
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\n[✔] Insurance rate updated successfully.");
                                Console.ResetColor();
                                _fileHandler.SaveEmployees(_employees);
                                _fileHandler.SaveEmployeeHistories(_employeeHistories);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\n[!] Invalid rate. Please enter a number between 0 and 100.");
                                Console.ResetColor();
                            }
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n[!] Invalid option.");
                            Console.ResetColor();
                            break;
                    }
                }

                // Wait for HR to read the message before continuing
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
        }
        // This method manages leave requests, allowing HR to approve or reject them
        private void CalculatePayroll()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("+=========== CALCULATE PAYROLL ===========+");
            Console.ResetColor();

            Console.WriteLine("\n[1] Calculate Single Payroll");
            Console.WriteLine("[2] Generate Monthly Payrolls");
            Console.Write("\nSelect option (1-2): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CalculateSinglePayroll();
                    break;
                case "2":
                    GenerateMonthlyPayrolls();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n[!] Invalid option. Press Enter to return.");
                    Console.ResetColor();
                    Console.ReadLine();
                    return;
            }
        }

        private void CalculateSinglePayroll()
        {
            // Show list of employees with their IDs
            Console.WriteLine("\nAvailable Employees:");
            Console.WriteLine("-------------------");
            foreach (var emp in _employees)
            {
                Console.WriteLine($"[{emp.EmployeeId:D4}] {emp.Name} - {emp.Position}");
            }
            Console.WriteLine("-------------------");

            // Ask for the employee ID
            Console.Write("\nEnter Employee ID: ");
            if (!int.TryParse(Console.ReadLine(), out int employeeId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[!] Invalid ID format. Press Enter to return.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            // Look for the employee by ID
            Employee selectedEmployee = null;
            foreach (var e in _employees)
            {
                if (e.EmployeeId == employeeId)
                {
                    selectedEmployee = e;
                    break;
                }
            }

            if (selectedEmployee == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[!] Employee not found. Press Enter to return.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            // Get current month
            string currentMonth = DateTime.Now.ToString("yyyy-MM");

            // Check for existing payroll
            var existingPayrolls = _fileHandler.LoadPayrollRecords()
                .Where(p => p.EmployeeId == employeeId && p.Month == currentMonth)
                .ToList();

            if (existingPayrolls.Any())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n[!] A payroll already exists for {selectedEmployee.Name} in {currentMonth}");
                Console.WriteLine("Options:");
                Console.WriteLine("[1] View existing payroll");
                Console.WriteLine("[2] Regenerate payroll");
                Console.WriteLine("[3] Cancel");
                Console.Write("\nSelect an option (1-3): ");
                Console.ResetColor();

                string option = Console.ReadLine();
                if (option == "1")
                {
                    // Display the existing payroll
                    var existingPayroll = existingPayrolls.First();
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n+=========== EXISTING PAYROLL ===========+");
                    Console.ResetColor();

                    Console.WriteLine($"\nEmployee ID    : {selectedEmployee.EmployeeId:D4}");
                    Console.WriteLine($"Name           : {selectedEmployee.Name}");
                    Console.WriteLine($"Position       : {selectedEmployee.Position}");
                    Console.WriteLine($"Department     : {selectedEmployee.Department}");
                    Console.WriteLine($"Month          : {currentMonth}");

                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine("Work Summary:");
                    Console.WriteLine($"- Days Worked      : {existingPayroll.DaysWorked}");
                    Console.WriteLine($"- Days on Leave    : {existingPayroll.DaysOnLeave}");
                    Console.WriteLine($"- Total Days Paid  : {existingPayroll.DaysWorked + existingPayroll.DaysOnLeave} of {selectedEmployee.ExpectedMonthlyWorkingDays}");

                    // Salary Details
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine("Salary Details:");
                    Console.WriteLine($"- Base Annual Salary   : ${selectedEmployee.Salary:F2}");
                    Console.WriteLine($"- Base Monthly Salary  : ${selectedEmployee.Salary / 12:F2}");
                    Console.WriteLine($"- Daily Rate (20 days) : ${(selectedEmployee.Salary / 12) / selectedEmployee.ExpectedMonthlyWorkingDays:F2}");

                    // This Month Base Pay
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine("This Month Base Pay:");
                    Console.WriteLine($"- Base Pay ({existingPayroll.DaysWorked} days)   : ${existingPayroll.BaseSalary:F2}");
                    Console.WriteLine($"- Bonus                : ${existingPayroll.Bonus:F2}");

                    // Benefits
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine("Benefits (Add to Pay):");
                    if (selectedEmployee.HasHealthInsurance)
                        Console.WriteLine($"- Health Insurance      : +${existingPayroll.BaseSalary * selectedEmployee.InsuranceRate:F2} ({selectedEmployee.InsuranceRate:P0} of monthly salary)");
                    if (selectedEmployee.HasDentalInsurance)
                        Console.WriteLine($"- Dental Insurance      : +${existingPayroll.BaseSalary * selectedEmployee.InsuranceRate:F2} ({selectedEmployee.InsuranceRate:P0} of monthly salary)");
                    if (selectedEmployee.HasVisionInsurance)
                        Console.WriteLine($"- Vision Insurance      : +${existingPayroll.BaseSalary * selectedEmployee.InsuranceRate:F2} ({selectedEmployee.InsuranceRate:P0} of monthly salary)");

                    // Deductions
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine("Deductions (Subtract from Pay):");
                    Console.WriteLine($"- Income Tax            : -${existingPayroll.TaxDeduction:F2} ({selectedEmployee.TaxRate:P0} of base pay)");
                    if (selectedEmployee.HasHealthInsurance || selectedEmployee.HasDentalInsurance || selectedEmployee.HasVisionInsurance)
                        Console.WriteLine($"- Insurance             : -${existingPayroll.InsuranceDeduction:F2} ({selectedEmployee.InsuranceRate:P0} of monthly salary)");

                    // Net Pay Calculation
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine("Net Pay (Base + Bonus + Benefits - Deductions):");
                    Console.Write($"= ${existingPayroll.BaseSalary:F2} + ${existingPayroll.Bonus:F2}");
                    if (selectedEmployee.HasHealthInsurance || selectedEmployee.HasDentalInsurance || selectedEmployee.HasVisionInsurance)
                        Console.Write($" + ${existingPayroll.InsuranceDeduction:F2}");
                    Console.WriteLine($" - ${existingPayroll.TaxDeduction + existingPayroll.InsuranceDeduction:F2}");

                    // Total Net Pay
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nTOTAL = ${existingPayroll.NetPay:F2}");
                    Console.ResetColor();

                    // Employer Contributions
                    Console.WriteLine("\nEmployer Contributions:");
                    if (selectedEmployee.HasSuperannuation)
                        Console.WriteLine($"- Superannuation ({selectedEmployee.InsuranceRate:P0}) : ${existingPayroll.Superannuation:F2}");
                    else
                        Console.WriteLine("NONE");

                    Console.WriteLine("\nPress Enter to return.");
                    Console.ReadLine();
                    return;
                }
                else if (option == "2")
                {
                    // Remove the existing payroll
                    var allPayrolls = _fileHandler.LoadPayrollRecords();
                    allPayrolls.RemoveAll(p => p.EmployeeId == employeeId && p.Month == currentMonth);
                    File.WriteAllText(Path.Combine("data", "payrolls.txt"), string.Empty); // Clear the file
                    foreach (var p in allPayrolls)
                    {
                        _fileHandler.SavePayrollRecord(p);
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n[✔] Existing payroll removed. Proceeding with new payroll calculation...");
                    Console.ResetColor();
                    Thread.Sleep(1000); // Give user time to read the message
                }
                else
                {
                    return;
                }
            }

            // Clear screen after employee selection
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("+=========== CALCULATE PAYROLL ===========+");
            Console.ResetColor();

            // Load and display check-in information
            var checkIns = _attendance
                .Where(a => a.EmployeeName.Equals(selectedEmployee.Name, StringComparison.OrdinalIgnoreCase) &&
                           a.Date.StartsWith(currentMonth))
                .ToList();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n*** ATTENDANCE INFORMATION FOR {selectedEmployee.Name} (ID: {employeeId:D4}) ***");
            Console.WriteLine($"Current Month: {currentMonth}");
            Console.WriteLine($"Total Check-ins Recorded: {checkIns.Count} days");

            if (checkIns.Any())
            {
                Console.WriteLine("\nCheck-in Dates:");
                foreach (var checkIn in checkIns.OrderBy(c => c.Date))
                {
                    Console.WriteLine($"- {checkIn.Date} ({checkIn.Status})");
                }
            }
            else
            {
                Console.WriteLine("\nNo check-ins recorded for this month.");
            }

            Console.WriteLine("\n*** This information is for reference only and won't affect payroll calculation ***");
            Console.ResetColor();
            Console.WriteLine("\n----------------------------------------");

            // Count days on leave for this month
            var leaveRequests = _leaveRequests
                .Where(r => (r.EmployeeName.Equals(selectedEmployee.Name, StringComparison.OrdinalIgnoreCase) ||
                           r.EmployeeId == selectedEmployee.EmployeeId) &&
                           r.Status.Equals("Approved", StringComparison.OrdinalIgnoreCase) &&
                           r.Date.StartsWith(currentMonth))
                .ToList();

            int daysOnLeave = leaveRequests.Sum(r => r.DaysRequested);
            // Display leave information
            Console.WriteLine($"\nTotal Days on Leave this month: {daysOnLeave}");
            if (leaveRequests.Any())
            {
                Console.WriteLine("\nLeave Dates:");
                foreach (var leave in leaveRequests.OrderBy(l => l.Date))
                {
                    Console.WriteLine($"- {leave.Date}: {leave.DaysRequested} days ({leave.Reason})");
                }
            }

            // Get additional payroll information
            Console.Write($"\nEnter days worked this month (expected {selectedEmployee.ExpectedMonthlyWorkingDays} days): ");
            if (!int.TryParse(Console.ReadLine(), out int daysWorked))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[!] Invalid input. Press Enter to return.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            Console.Write("Enter bonus amount (if any): ");
            if (!double.TryParse(Console.ReadLine(), out double bonus))
            {
                bonus = 0;
            }

            // Calculate monthly salary and deductions
            double monthlySalary = selectedEmployee.Salary / 12;
            double dailyRate = monthlySalary / selectedEmployee.ExpectedMonthlyWorkingDays;

            // Calculate total days (worked + approved leave)
            int totalDays = daysWorked + daysOnLeave;
            double baseSalary = dailyRate * totalDays;

            // Calculate insurance and tax deductions
            double insuranceDeduction = baseSalary * selectedEmployee.InsuranceRate;
            double taxDeduction = (baseSalary - insuranceDeduction) * selectedEmployee.TaxRate;

            // Calculate superannuation (employer contribution)
            double superannuation = selectedEmployee.HasSuperannuation ? baseSalary * 0.115 : 0;

            double netPay = baseSalary + bonus - insuranceDeduction - taxDeduction;

            // Display detailed payroll information
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n+=========== PAYROLL SUMMARY ===========+");
            Console.ResetColor();

            // Employee Info
            Console.WriteLine($"\nEmployee ID    : {selectedEmployee.EmployeeId:D4}");
            Console.WriteLine($"Name           : {selectedEmployee.Name}");
            Console.WriteLine($"Position       : {selectedEmployee.Position}");
            Console.WriteLine($"Department     : {selectedEmployee.Department}");
            Console.WriteLine($"Month          : {currentMonth}");

            // Work Summary
            Console.WriteLine("\n----------------------------------------");
            Console.WriteLine("Work Summary:");
            Console.WriteLine($"- Days Worked      : {daysWorked}");
            Console.WriteLine($"- Days on Leave    : {daysOnLeave}");
            Console.WriteLine($"- Total Days Paid  : {totalDays} of {selectedEmployee.ExpectedMonthlyWorkingDays}");

            // Salary Details
            Console.WriteLine("\n----------------------------------------");
            Console.WriteLine("Salary Details:");
            Console.WriteLine($"- Base Annual Salary   : ${selectedEmployee.Salary:F2}");
            Console.WriteLine($"- Base Monthly Salary  : ${monthlySalary:F2}");
            Console.WriteLine($"- Daily Rate (20 days) : ${dailyRate:F2}");

            // This Month Base Pay
            Console.WriteLine("\n----------------------------------------");
            Console.WriteLine("This Month Base Pay:");
            Console.WriteLine($"- Base Pay ({daysWorked} days)   : ${baseSalary:F2}");
            Console.WriteLine($"- Bonus                : ${bonus:F2}");

            // Benefits
            Console.WriteLine("\n----------------------------------------");
            Console.WriteLine("Benefits (Add to Pay):");
            if (selectedEmployee.HasHealthInsurance)
                Console.WriteLine($"- Health Insurance      : +${baseSalary * selectedEmployee.InsuranceRate:F2} ({selectedEmployee.InsuranceRate:P0} of monthly salary)");
            if (selectedEmployee.HasDentalInsurance)
                Console.WriteLine($"- Dental Insurance      : +${baseSalary * selectedEmployee.InsuranceRate:F2} ({selectedEmployee.InsuranceRate:P0} of monthly salary)");
            if (selectedEmployee.HasVisionInsurance)
                Console.WriteLine($"- Vision Insurance      : +${baseSalary * selectedEmployee.InsuranceRate:F2} ({selectedEmployee.InsuranceRate:P0} of monthly salary)");

            // Deductions
            Console.WriteLine("\n----------------------------------------");
            Console.WriteLine("Deductions (Subtract from Pay):");
            Console.WriteLine($"- Income Tax            : -${taxDeduction:F2} ({selectedEmployee.TaxRate:P0} of base pay)");
            if (selectedEmployee.HasHealthInsurance || selectedEmployee.HasDentalInsurance || selectedEmployee.HasVisionInsurance)
                Console.WriteLine($"- Insurance             : -${insuranceDeduction:F2} ({selectedEmployee.InsuranceRate:P0} of monthly salary)");

            // Net Pay Calculation
            Console.WriteLine("\n----------------------------------------");
            Console.WriteLine("Net Pay (Base + Bonus + Benefits - Deductions):");
            Console.Write($"= ${baseSalary:F2} + ${bonus:F2}");
            if (selectedEmployee.HasHealthInsurance || selectedEmployee.HasDentalInsurance || selectedEmployee.HasVisionInsurance)
                Console.Write($" + ${insuranceDeduction:F2}");
            Console.WriteLine($" - ${taxDeduction + insuranceDeduction:F2}");

            // Total Net Pay
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nTOTAL = ${netPay:F2}");
            Console.ResetColor();

            // Employer Contributions
            Console.WriteLine("\nEmployer Contributions:");
            if (selectedEmployee.HasSuperannuation)
                Console.WriteLine($"- Superannuation ({selectedEmployee.InsuranceRate:P0}) : ${superannuation:F2}");
            else
                Console.WriteLine("NONE");

            // Create and save payroll record
            var payroll = new PayrollRecord(
                selectedEmployee.EmployeeId,
                currentMonth,
                totalDays,
                daysOnLeave,
                baseSalary,
                bonus,
                insuranceDeduction,
                taxDeduction,
                netPay,
                superannuation
            );

            // Save payroll record
            _fileHandler.SavePayrollRecord(payroll);

            Console.WriteLine("\nPress Enter to return.");
            Console.ReadLine();
        }

        private void GenerateMonthlyPayrolls()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("+=========== GENERATE MONTHLY PAYROLLS ===========+");
            Console.ResetColor();

            // Get target month
            Console.Write("\nEnter target month (YYYY-MM): ");
            string targetMonth = Console.ReadLine();
            if (!DateTime.TryParseExact(targetMonth, "yyyy-MM", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[!] Invalid month format. Use YYYY-MM (e.g., 2024-03)");
                Console.ResetColor();
                Console.WriteLine("\nPress Enter to return.");
                Console.ReadLine();
                return;
            }

            // Check if payrolls already exist for this month
            var existingPayrolls = _fileHandler.LoadPayrollRecords()
                .Where(p => p.Month == targetMonth)
                .ToList();

            if (existingPayrolls.Any())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n[!] Payrolls already exist for {targetMonth}");
                Console.WriteLine("Do you want to regenerate them? (Y/N): ");
                Console.ResetColor();

                if (Console.ReadLine().ToUpper() != "Y")
                {
                    return;
                }
            }

            Console.WriteLine("\nGenerating payrolls for all employees...");
            int successCount = 0;
            int failCount = 0;

            foreach (var employee in _employees)
            {
                try
                {
                    // Count days on leave for this month
                    int daysOnLeave = _fileHandler.LoadLeaveRequests()
                        .Where(r => r.EmployeeName.ToLower() == employee.Name.ToLower() &&
                                   r.Status.ToLower() == "approved" &&
                                   r.Date.StartsWith(targetMonth))
                        .Sum(r => r.DaysRequested);

                    // Calculate monthly salary and deductions
                    double monthlySalary = employee.Salary / 12;
                    double dailyRate = monthlySalary / employee.ExpectedMonthlyWorkingDays;

                    // Assume full month worked unless on leave
                    int daysWorked = employee.ExpectedMonthlyWorkingDays - daysOnLeave;
                    int totalDays = daysWorked + daysOnLeave;
                    double baseSalary = dailyRate * totalDays;

                    // Calculate insurance and tax deductions
                    double insuranceDeduction = baseSalary * employee.InsuranceRate;
                    double taxDeduction = (baseSalary - insuranceDeduction) * employee.TaxRate;

                    // Calculate superannuation (employer contribution)
                    double superannuation = employee.HasSuperannuation ? baseSalary * 0.115 : 0;

                    double netPay = baseSalary - insuranceDeduction - taxDeduction;

                    // Create and save payroll record
                    var payroll = new PayrollRecord(
                        employee.EmployeeId,
                        targetMonth,
                        totalDays,
                        daysOnLeave,
                        baseSalary,
                        0, // No bonus for recurring payrolls
                        insuranceDeduction,
                        taxDeduction,
                        netPay,
                        superannuation
                    );

                    _fileHandler.SavePayrollRecord(payroll);
                    successCount++;
                }
                catch (Exception)
                {
                    failCount++;
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n[✔] Generated {successCount} payrolls successfully.");
            if (failCount > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[!] Failed to generate {failCount} payrolls.");
            }
            Console.ResetColor();

            Console.WriteLine("\nPress Enter to return.");
            Console.ReadLine();
        }

        private void ManageLeaveRequests()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("+========= LEAVE REQUESTS ==========+");
            Console.ResetColor();

            foreach (var req in _leaveRequests)
            {
                Console.WriteLine($"Leave ID: {req.LeaveId:D4} | {req.EmployeeName} - {req.DaysRequested} days - {req.Status}");
                Console.WriteLine("----------------------------------------");
            }

            Console.Write("\nEnter Leave ID to process (press Enter to return): ");
            string input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
                return;

            if (!int.TryParse(input, out int id))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[!] Invalid ID format. Press Enter to return.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

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
                    var employee = _employees.FirstOrDefault(e => e.EmployeeId == request.EmployeeId);
                    if (employee != null && employee.LeaveBalance >= request.DaysRequested)
                    {
                        employee.LeaveBalance -= request.DaysRequested;
                        _employeeHistories.Add(new EmployeeHistory(
                            employee.Name,
                            "Leave Approved",
                            $"Approved {request.DaysRequested} days leave. New balance: {employee.LeaveBalance} days",
                            employee.EmployeeId
                        ));
                        _fileHandler.SaveEmployees(_employees);
                        _fileHandler.SaveEmployeeHistories(_employeeHistories);
                    }
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

            _fileHandler.SaveLeaveRequests(_leaveRequests);
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

            // Show list of employees with their IDs
            Console.WriteLine("\nAvailable Employees:");
            Console.WriteLine("-------------------");
            foreach (var emp in _employees)
            {
                Console.WriteLine($"[{emp.EmployeeId:D4}] {emp.Name} - {emp.Position}");
            }
            Console.WriteLine("-------------------");

            // Ask for the employee ID
            Console.Write("\nEnter Employee ID: ");
            if (!int.TryParse(Console.ReadLine(), out int employeeId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[!] Invalid ID format. Press Enter to return.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            // Find employee name from ID
            string employeeName = null;
            foreach (var e in _employees)
            {
                if (e.EmployeeId == employeeId)
                {
                    employeeName = e.Name;
                    break;
                }
            }

            if (employeeName == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[!] Employee not found. Press Enter to return.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            Console.WriteLine();
            foreach (var record in _employeeHistories)
            {
                if (record.EmployeeName.ToLower() == employeeName.ToLower())
                {
                    Console.WriteLine(record);
                }
            }

            Console.WriteLine("\nPress Enter to return.");
            Console.ReadLine();
        }

        private void ViewPayrollHistory()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("+=========== PAYROLL HISTORY ===========+");
            Console.ResetColor();

            // Load all payroll records
            var payrollRecords = _fileHandler.LoadPayrollRecords();

            if (!payrollRecords.Any())
            {
                Console.WriteLine("\nNo payroll records found.");
                Console.WriteLine("\nPress Enter to return.");
                Console.ReadLine();
                return;
            }

            // Show list of employees with their IDs
            Console.WriteLine("\nAvailable Employees:");
            Console.WriteLine("-------------------");
            foreach (var emp in _employees)
            {
                Console.WriteLine($"[{emp.EmployeeId:D4}] {emp.Name} - {emp.Position}");
            }
            Console.WriteLine("[0] View All Employees");
            Console.WriteLine("-------------------");

            // Ask for the employee ID
            Console.Write("\nEnter Employee ID (0 for all): ");
            if (!int.TryParse(Console.ReadLine(), out int employeeId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n[!] Invalid ID format. Press Enter to return.");
                Console.ResetColor();
                Console.ReadLine();
                return;
            }

            // Get date range
            Console.Write("\nEnter start month (YYYY-MM) or press Enter for all: ");
            string startMonth = Console.ReadLine();
            Console.Write("Enter end month (YYYY-MM) or press Enter for all: ");
            string endMonth = Console.ReadLine();

            // Filter records based on selection
            var filteredRecords = payrollRecords;

            // Filter by employee if specific ID selected
            if (employeeId != 0)
            {
                filteredRecords = filteredRecords.Where(p => p.EmployeeId == employeeId).ToList();
                if (!filteredRecords.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\n[!] No payroll records found for this employee.");
                    Console.ResetColor();
                    Console.WriteLine("\nPress Enter to return.");
                    Console.ReadLine();
                    return;
                }
            }

            // Filter by date range if provided
            if (!string.IsNullOrWhiteSpace(startMonth))
            {
                filteredRecords = filteredRecords.Where(p => p.Month.CompareTo(startMonth) >= 0).ToList();
            }
            if (!string.IsNullOrWhiteSpace(endMonth))
            {
                filteredRecords = filteredRecords.Where(p => p.Month.CompareTo(endMonth) <= 0).ToList();
            }

            // Group records by month
            var recordsByMonth = filteredRecords
                .GroupBy(p => p.Month)
                .OrderByDescending(g => g.Key);

            // Display filtered results
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("+=========== PAYROLL HISTORY ===========+");
            Console.ResetColor();

            if (employeeId != 0)
            {
                var employee = _employees.FirstOrDefault(e => e.EmployeeId == employeeId);
                Console.WriteLine($"\nShowing payrolls for: {employee?.Name} (ID: {employeeId:D4})");
            }
            if (!string.IsNullOrWhiteSpace(startMonth) || !string.IsNullOrWhiteSpace(endMonth))
            {
                Console.WriteLine($"Date Range: {startMonth ?? "Start"} to {endMonth ?? "End"}");
            }
            Console.WriteLine("----------------------------------------");

            foreach (var monthGroup in recordsByMonth)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n=== {monthGroup.Key} ===");
                Console.ResetColor();

                foreach (var record in monthGroup.OrderBy(r => r.EmployeeId))
                {
                    // Find employee name
                    var employee = _employees.FirstOrDefault(e => e.EmployeeId == record.EmployeeId);
                    string employeeName = employee?.Name ?? "Unknown";

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n+==========================================+");
                    Console.WriteLine($"|           PAYROLL FOR {employeeName,-20} |");
                    Console.WriteLine("+==========================================+");
                    Console.ResetColor();

                    Console.WriteLine($"\nEmployee: {employeeName} (ID: {record.EmployeeId:D4})");
                    Console.WriteLine($"Position       : {employee?.Position ?? "N/A"}");
                    Console.WriteLine($"Department     : {employee?.Department ?? "N/A"}");
                    Console.WriteLine($"Month          : {record.Month}");

                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine("Work Summary:");
                    Console.WriteLine($"- Days Worked      : {record.DaysWorked}");
                    Console.WriteLine($"- Days on Leave    : {record.DaysOnLeave}");
                    Console.WriteLine($"- Total Days Paid  : {record.DaysWorked + record.DaysOnLeave} of {employee?.ExpectedMonthlyWorkingDays ?? 0}");

                    // Salary Details
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine("Salary Details:");
                    Console.WriteLine($"- Base Annual Salary   : ${employee?.Salary ?? 0:F2}");
                    Console.WriteLine($"- Base Monthly Salary  : ${(employee?.Salary ?? 0) / 12:F2}");
                    Console.WriteLine($"- Daily Rate (20 days) : ${((employee?.Salary ?? 0) / 12) / (employee?.ExpectedMonthlyWorkingDays ?? 20):F2}");

                    // This Month Base Pay
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine("This Month Base Pay:");
                    Console.WriteLine($"- Base Pay ({record.DaysWorked} days)   : ${record.BaseSalary:F2}");
                    Console.WriteLine($"- Bonus                : ${record.Bonus:F2}");

                    // Benefits
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine("Benefits (Add to Pay):");
                    if (employee?.HasHealthInsurance ?? false)
                        Console.WriteLine($"- Health Insurance      : +${record.BaseSalary * (employee?.InsuranceRate ?? 0):F2} ({(employee?.InsuranceRate ?? 0):P0} of monthly salary)");
                    if (employee?.HasDentalInsurance ?? false)
                        Console.WriteLine($"- Dental Insurance      : +${record.BaseSalary * (employee?.InsuranceRate ?? 0):F2} ({(employee?.InsuranceRate ?? 0):P0} of monthly salary)");
                    if (employee?.HasVisionInsurance ?? false)
                        Console.WriteLine($"- Vision Insurance      : +${record.BaseSalary * (employee?.InsuranceRate ?? 0):F2} ({(employee?.InsuranceRate ?? 0):P0} of monthly salary)");

                    // Deductions
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine("Deductions (Subtract from Pay):");
                    Console.WriteLine($"- Income Tax            : -${record.TaxDeduction:F2} ({(employee?.TaxRate ?? 0):P0} of base pay)");
                    if ((employee?.HasHealthInsurance ?? false) || (employee?.HasDentalInsurance ?? false) || (employee?.HasVisionInsurance ?? false))
                        Console.WriteLine($"- Insurance             : -${record.InsuranceDeduction:F2} ({(employee?.InsuranceRate ?? 0):P0} of monthly salary)");

                    // Net Pay Calculation
                    Console.WriteLine("\n----------------------------------------");
                    Console.WriteLine("Net Pay (Base + Bonus + Benefits - Deductions):");
                    Console.Write($"= ${record.BaseSalary:F2} + ${record.Bonus:F2}");
                    if ((employee?.HasHealthInsurance ?? false) || (employee?.HasDentalInsurance ?? false) || (employee?.HasVisionInsurance ?? false))
                        Console.Write($" + ${record.InsuranceDeduction:F2}");
                    Console.WriteLine($" - ${record.TaxDeduction + record.InsuranceDeduction:F2}");

                    // Total Net Pay
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\nTOTAL = ${record.NetPay:F2}");
                    Console.ResetColor();

                    // Employer Contributions
                    Console.WriteLine("\nEmployer Contributions:");
                    if (employee?.HasSuperannuation ?? false)
                        Console.WriteLine($"- Superannuation ({(employee?.InsuranceRate ?? 0):P0}) : ${record.Superannuation:F2}");
                    else
                        Console.WriteLine("NONE");

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n+==========================================+");
                    Console.ResetColor();
                }
            }

            Console.WriteLine("\nPress Enter to return.");
            Console.ReadLine();
        }
    }
}
