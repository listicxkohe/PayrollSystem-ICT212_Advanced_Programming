namespace PayrollSystem.Models
{
    public class PayrollRecord
    {
        public int EmployeeId { get; set; }
        public string Month { get; set; }  // Format: "YYYY-MM"
        public int DaysWorked { get; set; }
        public int DaysOnLeave { get; set; }
        public double BaseSalary { get; set; }
        public double Bonus { get; set; }
        public double InsuranceDeduction { get; set; }
        public double TaxDeduction { get; set; }
        public double NetPay { get; set; }
        public double Superannuation { get; set; }

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