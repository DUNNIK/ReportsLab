﻿using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities.Employee;
using DAL.StorageLayer.Task;
using ReportsLab.BusinessLogicLayer.ReportingSystem;

namespace ReportsLab.BusinessLogicLayer.EmployeeSystem
{
    public class Employee : ISubordinate, IMethodsWithTasks
    {
        private readonly List<DayReport> _dayReports = new List<DayReport>();
        private IDirector _director;

        public Employee(string name, IDirector director)
        {
            EmployeeInfo = new DAL.Entities.Employee.Employee(name);
            _director = director;
            _director.AddNewSubordinate(this);
            EmployeeData.AllEmployees.Add(Id, EmployeeInfo);
        }

        public void CreateDayReport(string name)
        {
            var report = new DayReport(Id, LastReportTime());
            _dayReports.Add(report);
            report.CreateReport(name);
        }

        public void CreateSprintReport(string name)
        {
            var sprintReport = new SprintReport(Id, _dayReports);
            sprintReport.CreateReport(name);
        }

        public string CreateTask(string name, string description)
        {
            return TaskManagementSystem.TaskManagementSystem.CreateTask(name, description);
        }

        public void OpenTask(string id)
        {
            TaskManagementSystem.TaskManagementSystem.OpenTask(this, id);
        }

        public void ActiveTask(string id)
        {
            TaskManagementSystem.TaskManagementSystem.ActiveTask(this, id);
        }

        public void ResolveTask(string id)
        {
            TaskManagementSystem.TaskManagementSystem.ResolveTask(this, id);
        }

        public void CreateCommit(string taskId, string commit)
        {
            TaskManagementSystem.TaskManagementSystem.CreateCommit(this, taskId, commit);
        }

        public Task GetTask(string id)
        {
            return TaskManagementSystem.TaskManagementSystem.Task(id)._task;
        }

        public List<Task> MyTasks()
        {
            return TaskManagementSystem.TaskManagementSystem.TasksEmployee(Id);
        }

        public List<Task> TasksByChangeCreateTime(DateTime dateTime)
        {
            return TaskManagementSystem.TaskManagementSystem.TasksByChangeCreateTime(dateTime);
        }

        public List<Task> TasksByLastChangeTime(DateTime changesTime)
        {
            return TaskManagementSystem.TaskManagementSystem.TasksByLastChangeTime(changesTime);
        }

        public List<Task> TasksEmployeeChanged(string employeeId)
        {
            return TaskManagementSystem.TaskManagementSystem.TasksEmployeeChanged(employeeId);
        }

        public DAL.StorageLayer.Infrastructure.IEmployee EmployeeInfo { get; }
        public string Id { get; } = Guid.NewGuid().ToString();

        public string Hierarchy()
        {
            return $"{EmployeeInfo.Name} - Ordinary employee";
        }

        public void GetNewDirector(IDirector director)
        {
            _director = director;
        }

        public bool IsThereADirecter()
        {
            return _director != null;
        }

        public IDirector Director()
        {
            return _director;
        }

        public IEnumerable<Task> AllResolved()
        {
            var result = new List<Task>();
            AddMyResolvedTasks(result);
            return result;
        }

        private void AddMyResolvedTasks(ICollection<Task> result)
        {
            foreach (var task in from dayReport in _dayReports
                from task in dayReport.Tasks
                where !result.Contains(task)
                select task)
                result.Add(task);
        }

        private DateTime LastReportTime()
        {
            return _dayReports.Count == 0 ? DateTime.Today : _dayReports.Last().CreateTime;
        }
    }
}