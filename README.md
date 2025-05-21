# Employee Payroll Management System

**Subject:** ICT212 – Advanced Programming
**Project:** Console-Based Payroll and HR System

---

## Overview

The **Employee Payroll Management System** is a C# console application designed for managing employee records, attendance, payroll calculations, leave requests, and administrative control. It is a menu-driven program that allows role-based access to different modules (Admin, HR, Employee), all operated via a clean, structured console interface.

This project was developed as part of the **ICT212 – Advanced Programming** assignment, with a focus on modular code design, data persistence, and user-friendly console UI formatting.

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

* **Employee Panel**

  * Daily check-in
  * Submit leave requests
  * View leave status
  * View personal employee history

* **Data Storage**

  * Persistent flat file storage using `.txt` files for users, employees, leave, history, and attendance.

---

## File Structure

```
EmployeePayrollSystem/
│
├── Program.cs                 // Entry point
├── Core/
│   └── PayrollApp.cs         // Main controller logic
│
├── Modules/
│   ├── AdminModule.cs        // Admin menu & logic
│   ├── HRModule.cs           // HR menu & logic
│   ├── EmployeeModule.cs     // Employee menu & logic
│   └── LoginModule.cs        // Login screen & validation
│
├── Models/
│   ├── User.cs               // User login model
│   ├── Employee.cs           // Employee data model
│   ├── AttendanceRecord.cs   // Attendance data model
│   ├── LeaveRequest.cs       // Leave application model
│   └── EmployeeHistory.cs    // Change log/history model
│
├── Services/
│   └── FileHandler.cs        // Read/write operations to .txt files
│
├── data/
│   ├── users.txt             // User accounts
│   ├── employees.txt         // Employee records
│   ├── attendance.txt        // Attendance logs
│   ├── leaves.txt            // Leave requests
│   └── employee_histories.txt // Historical changes
```

---

## Technologies Used

* **Language:** C# (.NET 6 or later recommended)
* **Platform:** Console-based application (Windows)
* **Data Storage:** Local flat files (`.txt` files, parsed line-by-line)
* **Tools:** Visual Studio / Visual Studio Code

---

## How to Run

1. Clone or download the repository.
2. Open the project in Visual Studio.
3. Ensure the `/data/` folder exists in the root with all necessary `.txt` files.
4. Build and run the project.
5. Use one of the sample admin accounts to begin:

   ```
   Username: Kohe
   Password: admin
   ```

---

## Notes

* The UI has been formatted for a clear console layout with headers, boxes, and color-coded sections to simulate a desktop-style experience.
* Password input is masked with `*` in the login screen for realism.
* Console resizing is optional and not enforced for compatibility.

---

