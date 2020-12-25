using System.Collections.Generic;
using ReportsLab.BusinessLogicLayer.TaskManagementSystem;

namespace ReportsLab.BusinessLogicLayer.EmployeeSystem
{
    public interface ISubordinate : IEmployee
    {
        void GetNewDirector(IDirector director);
        bool IsThereADirecter();
        IDirector Director();
        List<Task> AllResolved();
    }
}