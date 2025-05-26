
# SmartHR Console

```
  _____                      _   _   _           
 /  ___|                    | | | | | |          
 \ `--. _ __ ___   __ _ _ __| |_| |_| |_ __      
  `--. \ '_ ` _ \ / _` | '__| __|  _  | '__|     
 /\__/ / | | | | | (_| | |  | |_| | | | |        
 \____/|_| |_| |_|\__,_|_|   \__\_| |_|_|        

          SMARTHR CONSOLE LOGIN          
```

**Subject:** ICT212 – Advanced Programming
**Project:** Console-Based Payroll and HR System

---

## Overview

The **SmartHR Console** is a C# console application for managing employee records, attendance, payroll calculations, leave requests, and administrative control. It features a menu-driven, role-based interface (Admin, HR, Employee) with modular code design, persistent data storage, and a user-friendly console UI.

---

## Features

* **Admin Panel**

  * View all employees
  * Add new employees
  * Create new user accounts

* **HR Panel**

  * View and edit employee records
  * Calculate payroll with bonuses and deductions
  * Manage and approve/reject leave requests
  * View employee history
  * Analytics & Reports (hardcoded department/employee analytics)

* **Employee Panel**

  * Daily check-in
  * Submit leave requests
  * View leave status
  * View personal employee history

* **Data Security**

  * User passwords are stored encrypted in the data files (`Services/Encryption.cs`)

* **Data Storage**

  * Persistent flat file storage using `.txt` files for users, employees, leave, history, and attendance

---

## File Structure

```
PayrollSystem-ICT212_Advanced_Programming/
│
├── Program.cs                      // Entry point
│
├── Core/
│   └── PayrollApp.cs               // Main controller logic
│
├── Modules/
│   ├── AdminModule.cs              // Admin menu & logic
│   ├── EmployeeModule.cs           // Employee menu & logic
│   ├── HRModule.cs                 // HR menu & logic (includes analytics/reports)
│   └── LoginModule.cs              // Login screen & validation
│
├── Models/
│   ├── AttendanceRecord.cs         // Attendance data model
│   ├── Employee.cs                 // Employee data model
│   ├── EmployeeHistory.cs          // Change log/history model
│   ├── LeaveRequest.cs             // Leave application model
│   ├── PayrollRecord.cs            // Payroll record model
│   └── User.cs                     // User login model
│
├── Services/
│   ├── FileHandler.cs              // Read/write operations to .txt files (with encryption)
│   └── Encryption.cs               // Handles password encryption/decryption
│
├── data/
│   ├── users.txt                   // User accounts (passwords encrypted)
│   ├── employees.txt               // Employee records
│   ├── attendance.txt              // Attendance logs
│   ├── leaves.txt                  // Leave requests
│   └── employee_histories.txt      // Historical changes
```

---

## Technologies Used

* **Language:** C# (.NET 8 recommended)
* **Platform:** Console-based application (Windows)
* **Data Storage:** Local flat files (`.txt` files, parsed line-by-line)
* **Security:** Password encryption for user accounts (`Services/Encryption.cs`)
* **Tools:** Visual Studio 2022 / Visual Studio Code

---

## How to Run

1. Clone or download the repository:

   ```
   https://github.com/listicxkohe/PayrollSystem-ICT212_Advanced_Programming
   ```
2. Open the solution in Visual Studio 2022.
3. Make sure the `/data/` folder exists in the root with all required `.txt` files.

   * Sample files are included (passwords are encrypted by default).
4. Build and run the project.
5. Log in using the sample admin account:

   ```
   Username: Kohe
   Password: admin
   ```

---

## Notes

* The UI uses headers, boxes, and color-coded sections for a clear console experience.
* Password input is masked with `*` on the login screen.
* Analytics and payroll reports are included in the HR panel with hardcoded sample data.
* Console window resizing is not enforced and is optional for display compatibility.

---

## License

This project was developed for academic purposes. 

---

## GitHub Repository

[https://github.com/listicxkohe/PayrollSystem-ICT212\_Advanced\_Programming](https://github.com/listicxkohe/PayrollSystem-ICT212_Advanced_Programming)

---

