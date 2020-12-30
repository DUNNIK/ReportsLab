using System.Collections.Generic;
using DAL.Entities.Task;

namespace ReportsLab.EmployeeSystem
{
    public interface ISubordinate : IEmployee
    {
        void GetNewDirector(IDirector director);
        bool IsThereADirector();
        IDirector Director();
        IEnumerable<Task> AllResolved();
    }
}