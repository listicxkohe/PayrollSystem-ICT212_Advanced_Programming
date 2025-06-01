namespace PayrollSystem.Models
{
    // This class represents one payroll entry for an employee per month
    public class PayrollRecord
    {
        // ID that links this record to an employee in the system
        public int EmployeeId { get; set; }

        // Format should always be "YYYY-MM" so we can sort easily
        public string Month { get; set; }

        // Number of days the employee actually worked this month
        public int DaysWorked { get; set; }

        // Days they were officially on leave (e.g. annual or sick leave)
        public int DaysOnLeave { get; set; }

        // Their regular monthly salary before any changes
        public double BaseSalary { get; set; }

        // Extra pay like performance bonus, commission, etc.
        public double Bonus { get; set; }

        // Deduct insurance contributions here (health, income protection, etc.)
        public double InsuranceDeduction { get; set; }

        // Tax deduction based on calculated tax rate (flat or progressive)
        public double TaxDeduction { get; set; }

        // What they actually take home after all deductions (Base + Bonus - Deductions)
        public double NetPay { get; set; }

        // Employer-paid super contribution for this payroll period
        public double Superannuation { get; set; }

        // Constructor – use this when creating a new payroll record from scratch
        public PayrollRecord(int employeeId, string month, int daysWorked, int daysOnLeave,
            double baseSalary, double bonus, double insuranceDeduction, double taxDeduction,
            double netPay, double superannuation)
        {
            EmployeeId = employeeId;
            Month = month;
            DaysWorked = daysWorked;
            DaysOnLeave = daysOnLeave;
            BaseSalary = baseSalary;
            Bonus = bonus;
            InsuranceDeduction = insuranceDeduction;
            TaxDeduction = taxDeduction;
            NetPay = netPay;
            Superannuation = superannuation;
        }

        // Just a nice string to dump out a full payroll record
        public override string ToString()
        {
            return $"Payroll for Employee {EmployeeId:D4} - {Month}\n" +
                   $"Days Worked: {DaysWorked}, Days on Leave: {DaysOnLeave}\n" +
                   $"Base Salary: ${BaseSalary:F2}\n" +
                   $"Bonus: ${Bonus:F2}\n" +
                   $"Insurance Deduction: ${InsuranceDeduction:F2}\n" +
                   $"Tax Deduction: ${TaxDeduction:F2}\n" +
                   $"Superannuation: ${Superannuation:F2}\n" +
                   $"Net Pay: ${NetPay:F2}";
        }
    }
}
