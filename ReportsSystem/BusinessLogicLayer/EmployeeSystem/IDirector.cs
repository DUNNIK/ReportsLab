using System.Collections.Generic;
using ReportsLab.BusinessLogicLayer.TaskManagementSystem;

namespace ReportsLab.BusinessLogicLayer.EmployeeSystem
{
    public interface IDirector : IEmployee
    {
        void AddNewSubordinate(ISubordinate subordinate);
        void TransferEmployeeToAnotherDirector(ISubordinate subordinate, IDirector newDirector);

        List<ISubordinate> Subordinates();
        List<Task> TasksAssignedToSubordinates();
        
        void UpdateTaskEmployee(string taskId, IEmployee assigned);
    }
}