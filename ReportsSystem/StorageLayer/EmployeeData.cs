using System.Collections.Generic;
using System.Linq;
using ReportsLab.BusinessLogicLayer.EmployeeSystem;

namespace ReportsLab.StorageLayer
{
    public static class EmployeeData
    {
        public static readonly List<IEmployee> TeamLeads = new List<IEmployee>();
        public static readonly Dictionary<string, IEmployee> AllEmployees = new Dictionary<string, IEmployee>();
        
        public static string Hierarchy()
        {
            return TeamLeads.Aggregate("", (current, teamLead) => current + (teamLead.Hierarchy() + "\n"));
        }
    }
}