using System.Collections.Generic;
using ReportsLab.BusinessLogicLayer.EmployeeSystem;
using ReportsLab.StorageLayer;
using static System.Console;
namespace ReportsLab
{
    static class Program
    {
        static void Main(string[] args)
        {
            var teamLead = new TeamLead("Nikita", new List<ISubordinate>());
            var director = new Director("Nail", teamLead, new List<ISubordinate>());
            var employee1 = new Employee("Nan", director);
            var employee2 = new Employee("Non", director);
            director.TransferEmployeeToAnotherDirector(employee2, teamLead);
            
            WriteLine(EmployeeData.Hierarchy());
        }
    }
}