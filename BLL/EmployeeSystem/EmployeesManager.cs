using System.Collections.Generic;
using System.Linq;
using DAL.Entities.Employee;
using ReportsLab.Exceptions;

namespace ReportsLab.EmployeeSystem
{
    public static class EmployeesManager
    {
        public static readonly Dictionary<string, Employee> AllOrdinaryEmployees = new Dictionary<string, Employee>();
        public static readonly Dictionary<string, Director> AllDirectors = new Dictionary<string, Director>();
        public static readonly Dictionary<string, TeamLead> AllTeamLeads = new Dictionary<string, TeamLead>();

        public static DAL.Infrastructure.IEmployee SearchEmployeeInData(string id)
        {
            return EmployeeData.AllEmployees[id];
        }

        public static string AllEmployees()
        {
            return EmployeeData.AllEmployees.Values.Aggregate("", (current, employees) => current + employees.ToString());
        }
        public static Employee SearchOrdinaryEmployee(string id)
        {
            if (!AllOrdinaryEmployees.ContainsKey(id)) throw new ManagerException();
            return AllOrdinaryEmployees[id];
        }
        public static Director SearchDirector(string id)
        {
            if (!AllDirectors.ContainsKey(id)) throw new ManagerException();
            return AllDirectors[id];
        }
        public static TeamLead SearchTeamLead(string id)
        {
            if (!AllTeamLeads.ContainsKey(id)) throw new ManagerException();
            return AllTeamLeads[id];
        }
    }
}