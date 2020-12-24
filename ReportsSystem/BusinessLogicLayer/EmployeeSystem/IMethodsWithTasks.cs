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
        void UpdateTaskEmployee(string taskId, IEmployee assigned);
        Task GetTask(string id);
        List<Task> MyTasks();
        List<Task> TasksByChangeCreateTime(DateTime dateTime);
        List<Task> TasksByLastChangeTime(DateTime changesTime);
        List<Task> TasksEmployeeChanged(string employeeId);
    }
}