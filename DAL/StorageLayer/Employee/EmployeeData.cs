using System.Collections.Generic;
using System.Linq;
using DAL.StorageLayer.Infrastructure;

namespace DAL.StorageLayer.Employee
{
    public static class EmployeeData
    {
        public static readonly List<IEmployee> TeamLeads = new List<IEmployee>();
        public static readonly Dictionary<string, IEmployee> AllEmployees = new Dictionary<string, IEmployee>();
    }
}