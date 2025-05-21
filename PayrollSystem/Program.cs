// Program.cs
using PayrollSystem.Core;

namespace PayrollSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            PayrollApp app = new PayrollApp();
            app.Start();
        }
    }
}