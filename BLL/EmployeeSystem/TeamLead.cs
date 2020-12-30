using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities.Employee;
using DAL.Entities.Task;
using ReportsLab.Exceptions;
using ReportsLab.ReportingSystem;

namespace ReportsLab.EmployeeSystem
{
    public class TeamLead : IDirector, IMethodsWithTasks
    {
        private SprintReport _sprintReport;
        private readonly List<DayReport> _dayReports = new List<DayReport>();
        private readonly List<ISubordinate> _subordinates;
        private List<Task> _resolvedTasks;
        public TeamLead(string name, List<ISubordinate> subordinates)
        {
            EmployeeInfo = new DAL.Entities.Employee.Employee(name);
            Id = EmployeeInfo.Id;
            _subordinates = subordinates;
            EmployeesManager.AllTeamLeads.Add(Id, this);
            EmployeeData.AllEmployees.Add(Id, EmployeeInfo);
        }

        public DAL.Infrastructure.IEmployee EmployeeInfo { get; }
        public string Id { get; }

        public string Hierarchy()
        {
            var resultString = $"{EmployeeInfo.Name} - Team lead\n";
            foreach (var subordinate in _subordinates) resultString += "\t" + subordinate.Hierarchy() + "\n";

            return resultString;
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

        public string CreateTask(string name, string description)
        {
            return TaskManagementSystem.TaskManagementSystem.CreateTask(name, description);
        }

        public void CreateDayReport(string name)
        {
            var report = new DayReport(Id, LastReportTime());
            _dayReports.Add(report);
            report.CreateReport(name);
        }

        public void CreateSprintReport(string name)
        {
            _resolvedTasks = FindTeamResolvedTasks();
            _sprintReport ??= new SprintReport(Id, _resolvedTasks);
            _sprintReport.CreateReport(name);
        }
        public void UpdateSprintReport()
        {
            _sprintReport.Update(_resolvedTasks);
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

        private DateTime LastReportTime()
        {
            return _dayReports.Count == 0 ? DateTime.Today : _dayReports.Last().CreateTime;
        }

        private List<Task> FindTeamResolvedTasks()
        {
            var result = new List<Task>();
            foreach (var task in from subordinate in _subordinates
                from task in subordinate.AllResolved()
                where !result.Contains(task)
                select task) result.Add(task);

            return result;
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