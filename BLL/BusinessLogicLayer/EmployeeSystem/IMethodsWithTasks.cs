using System;
using System.Collections.Generic;
using DAL.StorageLayer.Task;

namespace ReportsLab.BusinessLogicLayer.EmployeeSystem
{
    internal interface IMethodsWithTasks
    {
        string CreateTask(string name, string description);
        void CreateDayReport(string name);
        void CreateSprintReport(string name);
        void OpenTask(string id);
        void ActiveTask(string id);
        void ResolveTask(string id);
        void CreateCommit(string taskId, string commit);
        Task GetTask(string id);
        List<Task> MyTasks();
        List<Task> TasksByChangeCreateTime(DateTime dateTime);
        List<Task> TasksByLastChangeTime(DateTime changesTime);
        List<Task> TasksEmployeeChanged(string employeeId);
    }
}