
# SmartHR Console
current passwords:
kohe : admin (admin)
gasty : admin (admin)
bob : bob (employee)
Alice : alice (hr)

caear cipher shift 29 a --> d

## 📘 User Manual & Documentation

### 📑 Table of Contents
- Introduction
- System Requirements
- Installation & Setup
- Logging In
- User Roles & Access
- Menu Navigation & Layouts
- Admin Menu
- HR Menu
- Employee Menu
- Feature Walkthroughs
- Admin Features
- HR Features
- Employee Features
- Data Security & Storage
- Analytics & Reports
- Troubleshooting & FAQ
- File Structure
- License
- Repository

---

## Introduction
SmartHR Console is a robust, menu-driven C# (.NET 8) console application designed for managing employee records, attendance, payroll processing, leave requests, and administrative operations. With role-based access (Admin, HR, Employee), persistent encrypted storage, and a polished console UI, it simulates a complete HR and Payroll system in a terminal environment.

---

## System Requirements
- OS: Windows 10 or later
- .NET SDK: .NET 8 (or compatible runtime)
- IDE: Visual Studio 2022 (recommended) or Visual Studio Code

---

## Installation & Setup
1. Clone the Repository:
```bash
git clone https://github.com/listicxkohe/PayrollSystem-ICT212_Advanced_Programming.git
```
2. Open the Solution:
   - In Visual Studio: File > Open > Project/Solution
   - Select the `.sln` file in the root directory
3. Verify Data Files:
   - Ensure the `/data/` directory contains the necessary `.txt` files:
     - `users.txt`, `employees.txt`, `attendance.txt`, `leaves.txt`, `employee_histories.txt`
4. Build & Run:
   - Press F5 or navigate to Debug > Start Debugging

---

## Logging In
1. Launch the application from Visual Studio.
2. At the login prompt:
```
+=============================================+
|           SMARTHR CONSOLE LOGIN             |
+=============================================+
Username: [your_username]
Password: [your_password]
```
3. Enter the credentials for one of the sample accounts:
```
Username: admin
Password: admin
```
4. You will be redirected to the appropriate role-based menu.

---

## User Roles & Access
| Role     | Access Capabilities                                                                 |
|----------|----------------------------------------------------------------------------------------|
| Admin    | Manage users, view/add employees, create accounts                                     |
| HR       | View/edit employee data, calculate payroll, manage leave, analytics/reports          |
| Employee | Daily check-in, submit leave requests, view leave status and history                 |

---

## Menu Navigation & Layouts

### Admin Menu
```
+=============================================+
|                ADMIN MAIN MENU              |
+=============================================+
[1] View Employees
[2] Add Employee
[3] Create User Account
[4] Logout
```

### HR Menu
```
+=============================================+
|                 HR MAIN MENU                |
+=============================================+
[1] View Employees
[2] Edit Employee
[3] Calculate Payroll
[4] Manage Leave Requests
[5] View Employee History
[6] View Payroll History
[7] Analytics
[8] Logout
```

### Employee Menu
```
+=============================================+
|              EMPLOYEE MAIN MENU             |
+=============================================+
[1] Daily Check-In
[2] Submit Leave Request
[3] View Leave Status
[4] View My History
[5] Logout
```

---

## Feature Walkthroughs

### Admin Features
1. View Employees
   - Displays list with ID, name, position, department, and salary
2. Add Employee
   - Prompts for details, assigns unique ID
3. Create User Account
   - Adds login credentials (with encrypted password)
4. Logout
   - Returns to login screen

### HR Features
1. View Employees
   - Detailed view including benefits and leave
2. Edit Employee
   - Modify position, salary, benefits, insurance, etc.
3. Calculate Payroll
   - Per employee or bulk by month; includes net pay breakdown
4. Manage Leave Requests
   - Approve or reject requests by ID
5. View Employee History
   - Tracks changes: promotions, leave, salary, etc.
6. View Payroll History
   - Filter payroll by employee and date
7. Analytics
   - View hardcoded salary reports and growth trends
8. Logout

### Employee Features
1. Daily Check-In
   - Logs attendance for today
2. Submit Leave Request
   - Enter days, reason, start date; sends to HR
3. View Leave Status
   - Track approval of requests
4. View My History
   - See leave, payroll, status changes
5. Logout

---

## Data Security & Storage
- Passwords: Encrypted using Services/Encryption.cs
- Data Files:
  - `users.txt` – encrypted credentials
  - `employees.txt` – main employee records
  - `attendance.txt` – daily check-in logs
  - `leaves.txt` – leave applications
  - `employee_histories.txt` – audit logs
- Managed By: Services/FileHandler.cs

---

## Analytics & Reports
- Payroll Reports:
  - Department-wise salary and benefits (hardcoded)
- Salary Growth:
  - Hardcoded data showing growth per department or individual

---

## Troubleshooting & FAQ
| Issue                     | Solution                                                                 |
|--------------------------|--------------------------------------------------------------------------|
| Login fails              | Double-check credentials. Use sample admin login if testing.             |
| Missing data files       | Ensure `/data/` folder contains all `.txt` files.                        |
| Payroll not calculating  | Confirm attendance and leave data are populated.                        |
| Build errors             | Verify .NET 8 SDK is installed and selected as target framework.         |

---

## File Structure
```
PayrollSystem-ICT212_Advanced_Programming/
├── Program.cs
├── Core/
│   └── PayrollApp.cs
├── Modules/
│   ├── AdminModule.cs
│   ├── EmployeeModule.cs
│   ├── HRModule.cs
│   └── LoginModule.cs
├── Models/
│   ├── AttendanceRecord.cs
│   ├── Employee.cs
│   ├── EmployeeHistory.cs
│   ├── LeaveRequest.cs
│   ├── PayrollRecord.cs
│   └── User.cs
├── Services/
│   ├── FileHandler.cs
│   └── Encryption.cs
├── data/
│   ├── users.txt
│   ├── employees.txt
│   ├── attendance.txt
│   ├── leaves.txt
│   └── employee_histories.txt
└── Docs/
    └── UserManual.md
```

---

## License
This project was created for academic use under ICT212. Reuse or modification requires proper credit to the original authors.

---

## Repository
https://github.com/listicxkohe/PayrollSystem-ICT212_Advanced_Programming
