// Program.cs
using PayrollSystem.Core;
using PayrollSystem.Services;
using PayrollSystem.Modules;

namespace PayrollSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileHandler = new FileHandler();
            var users = fileHandler.LoadUsers();
            var employees = fileHandler.LoadEmployees();
            var payroll = fileHandler.LoadPayrollRecords();

            var loginModule = new LoginModule(users, fileHandler);

            var app = new PayrollApp();
            app.Run();
        }
    }
}