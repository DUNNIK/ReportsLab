using System;
using System.Collections.Generic;
using System.Linq;
using ReportsLab.BusinessLogicLayer.ReportingSystem;
using ReportsLab.BusinessLogicLayer.TaskManagementSystem;
using ReportsLab.StorageLayer;

namespace ReportsLab.BusinessLogicLayer.EmployeeSystem
{
    public class Employee : ISubordinate, IMethodsWithTasks
    {
        private List<DayReport> _dayReports = new List<DayReport>();
        private readonly string _name;
        private IDirector _director;

        public Employee(string name, IDirector director)
        {
            _name = name;
            _director = director;
            _director.AddNewSubordinate(this);
            EmployeeData.AllEmployees.Add(Id, this);
        }

        public string Id { get; } = Guid.NewGuid().ToString();

        public string Hierarchy()
        {
            return $"{_name} - Ordinary employee";
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

        public List<Task> AllResolved()
        {
            var result = new List<Task>();
            AddMyResolvedTasks(result);
            return result;
        }
        private void AddMyResolvedTasks(ICollection<Task> result){
            foreach (var task in from dayReport in _dayReports
                from task in dayReport.Tasks
                where !result.Contains(task)
                select task)
            {
                result.Add(task);
            }
        }
        public void CreateDayReport(string name)
        {
            var report = new DayReport(Id, _dayReports.Last().CreateTime);
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
            return TaskManagementSystem.TaskManagementSystem.CreateTask(this, name, description);
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
            return TaskManagementSystem.TaskManagementSystem.Task(id);
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
    }
}