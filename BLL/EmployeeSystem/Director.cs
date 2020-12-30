using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities.Employee;
using DAL.Entities.Task;
using ReportsLab.Exceptions;
using ReportsLab.ReportingSystem;

namespace ReportsLab.EmployeeSystem
{
    public class Director : IDirector, ISubordinate, IMethodsWithTasks
    {
        private SprintReport _sprintReport;
        private readonly List<DayReport> _dayReports = new List<DayReport>();
        private readonly List<ISubordinate> _subordinates;
        private IDirector _director;

        public Director(string name, IDirector director, List<ISubordinate> subordinates)
        {
            EmployeeInfo = new DAL.Entities.Employee.Employee(name);
            Id = EmployeeInfo.Id;
            _director = director;
            _subordinates = subordinates;
            _director.AddNewSubordinate(this);
            EmployeesManager.AllDirectors.Add(Id, this);
            EmployeeData.AllEmployees.Add(Id, EmployeeInfo);
        }

        public DAL.Infrastructure.IEmployee EmployeeInfo { get; }
        public string Id { get; }

        public string Hierarchy()
        {
            var resultString = $"{EmployeeInfo} - director\n";
            foreach (var subordinate in _subordinates) resultString += "\t\t" + subordinate.Hierarchy() + "\n";

            return resultString;
        }

        public void AddNewSubordinate(ISubordinate subordinate)
        {
            //if (subordinate.IsThereADirecter()) throw new DirecterException();
            _subordinates.Add(subordinate);
            subordinate.GetNewDirector(this);
        }

        public void TransferEmployeeToAnotherDirector(ISubordinate subordinate, IDirector newDirector)
        {
            if (_subordinates.Remove(subordinate))
                newDirector.AddNewSubordinate(subordinate);
            else
                throw new SubordinateException();
        }

        public List<ISubordinate> Subordinates()
        {
            return _subordinates;
        }

        public void UpdateTaskEmployee(string taskId, IEmployee assigned)
        {
            if (!IsMySubordinate(assigned)) throw new EmployeeUpdateException();
            TaskManagementSystem.TaskManagementSystem.UpdateTaskEmployee(this, taskId, assigned);
        }

        public List<Task> TasksAssignedToSubordinates()
        {
            return TaskManagementSystem.TaskManagementSystem.TasksAssignedToSubordinates(this);
        }

        public void CreateDayReport(string name)
        {
            var report = new DayReport(Id, LastReportTime());
            _dayReports.Add(report);
            report.CreateReport(name);
        }

        public void UpdateSprintReport()
        {
            _sprintReport.Update(_dayReports);
        }
        public void CreateSprintReport(string name)
        {
            _sprintReport ??= new SprintReport(Id, _dayReports);
            _sprintReport.CreateReport(name);
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

        public IEnumerable<Task> MyTasks()
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

        public IEnumerable<Task> AllResolved()
        {
            var result = new List<Task>();
            AddMyResolvedTasks(result);
            AddSubordinatesResolvedTasks(result);
            return result;
        }

        public void GetNewDirector(IDirector newDirector)
        {
            _director = newDirector;
        }

        public bool IsThereADirector()
        {
            return _director != null;
        }

        IDirector ISubordinate.Director()
        {
            return _director;
        }

        private void AddMyResolvedTasks(ICollection<Task> result)
        {
            foreach (var task in from dayReport in _dayReports
                from task in dayReport.Tasks
                where !result.Contains(task)
                select task)
                result.Add(task);
        }

        private void AddSubordinatesResolvedTasks(ICollection<Task> result)
        {
            foreach (var task in from subordinate in _subordinates
                from task in subordinate.AllResolved()
                where !result.Contains(task)
                select task)
                result.Add(task);
        }

        private DateTime LastReportTime()
        {
            return _dayReports.Count == 0 ? DateTime.Today : _dayReports.Last().CreateTime;
        }

        private bool IsMySubordinate(IEmployee assigned)
        {
            return _subordinates.Contains(assigned);
        }

        public override string ToString()
        {
            return EmployeeInfo.Name + " - " + Id + "\n";
        }
    }
}