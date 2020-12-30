using System;
using System.Collections.Generic;
using DAL.Entities.Task;

namespace ReportsLab.EmployeeSystem
{
    public interface IMethodsWithTasks : IEmployee
    {
        string CreateTask(string name, string description);
        void CreateDayReport(string name);
        void CreateSprintReport(string name);
        void OpenTask(string id);
        void ActiveTask(string id);
        void ResolveTask(string id);
        void CreateCommit(string taskId, string commit);
        Task GetTask(string id);
        IEnumerable<Task> MyTasks();
        List<Task> TasksByChangeCreateTime(DateTime dateTime);
        List<Task> TasksByLastChangeTime(DateTime changesTime);
        List<Task> TasksEmployeeChanged(string employeeId);
    }
}