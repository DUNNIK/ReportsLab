using System;
using System.Collections.Generic;
using ReportsLab.BusinessLogicLayer.TaskManagementSystem;

namespace ReportsLab.BusinessLogicLayer.EmployeeSystem
{
    interface IMethodsWithTasks
    {
        string CreateTask(string name, string description);
        void CreateDayReport(string name);
        void CreateSprintReport(string name);
        void OpenTask(string id);
        void ActiveTask(string id);
        void ResolveTask(string id);
        void CreateCommit(string taskId, string commit);
        DAL.StorageLayer.Task.Task GetTask(string id);
        List<DAL.StorageLayer.Task.Task> MyTasks();
        List<DAL.StorageLayer.Task.Task> TasksByChangeCreateTime(DateTime dateTime);
        List<DAL.StorageLayer.Task.Task> TasksByLastChangeTime(DateTime changesTime);
        List<DAL.StorageLayer.Task.Task> TasksEmployeeChanged(string employeeId);
    }
}