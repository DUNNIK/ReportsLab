using System;

namespace ReportsLab.Exceptions
{
    public class EmployeeUpdateException : Exception
    {
        public EmployeeUpdateException() : base("There is no way to work with this subordinate")
        {
        }
    }
}