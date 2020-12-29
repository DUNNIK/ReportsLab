using System.Collections.Generic;
using DAL.StorageLayer.Task;

namespace ReportsLab.BusinessLogicLayer.EmployeeSystem
{
    public interface ISubordinate : IEmployee
    {
        void GetNewDirector(IDirector director);
        bool IsThereADirecter();
        IDirector Director();
        IEnumerable<Task> AllResolved();
    }
}