using System.Collections.Generic;
using ReportsLab.BusinessLogicLayer.TaskManagementSystem;

namespace ReportsLab.BusinessLogicLayer.EmployeeSystem
{
    public interface IEmployee
    {
        string Id { get; }
        string Hierarchy();
    }
}