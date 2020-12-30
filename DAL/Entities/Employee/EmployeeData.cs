using System.Collections.Generic;
using System.Linq;
using DAL.Infrastructure;

namespace DAL.Entities.Employee
{
    public static class EmployeeData
    {
        public static readonly Dictionary<string, IEmployee> AllEmployees = new Dictionary<string, IEmployee>();
        
    }
}