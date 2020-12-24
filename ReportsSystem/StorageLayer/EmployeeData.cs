using System.Collections.Generic;
using ReportsLab.BusinessLogicLayer.EmployeeSystem;

namespace ReportsLab.StorageLayer
{
    public static class EmployeeData
    {
        public static List<IEmployee> TeamLeads = new List<IEmployee>();
        public static Dictionary<string, IEmployee> AllEmployees = new Dictionary<string, IEmployee>();
        
        public static string Hierarchy()
        {
            var resultString = "";
            foreach (var teamLead in TeamLeads)
            {
                resultString += teamLead.Hierarchy() + "\n";
            }
            return resultString;
        }
    }
}