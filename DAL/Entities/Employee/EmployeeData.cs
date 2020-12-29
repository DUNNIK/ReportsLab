using System.Collections.Generic;
using DAL.StorageLayer.Infrastructure;

namespace DAL.Entities.Employee
{
    public static class EmployeeData
    {
        public static readonly List<IEmployee> TeamLeads = new List<IEmployee>();
        public static readonly Dictionary<string, IEmployee> AllEmployees = new Dictionary<string, IEmployee>();
    }
}