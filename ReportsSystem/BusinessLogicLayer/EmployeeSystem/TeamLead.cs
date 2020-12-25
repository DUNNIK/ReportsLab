using System;
using System.Collections.Generic;
using System.Linq;
using ReportsLab.BusinessLogicLayer.Exceptions;
using ReportsLab.BusinessLogicLayer.ReportingSystem;
using ReportsLab.BusinessLogicLayer.TaskManagementSystem;
using ReportsLab.StorageLayer;

namespace ReportsLab.BusinessLogicLayer.EmployeeSystem
{
    public class TeamLead : IDirector, IMethodsWithTasks
    {
        private readonly List<DayReport> _dayReports = new List<DayReport>();
        private readonly string _name;
        private readonly List<ISubordinate> _subordinates;

        public TeamLead(string name, List<ISubordinate> subordinates)
        {
            _name = name;
            _subordinates = subordinates;
            EmployeeData.TeamLeads.Add(this);
            EmployeeData.AllEmployees.Add(Id, this);
        }

        public string Id { get; } = Guid.NewGuid().ToString();

        public string Hierarchy()
        {
            string resultString = $"{_name} - Team lead\n";
            foreach (var subordinate in _subordinates)
            {
                resultString += "\t" + subordinate.Hierarchy() + "\n";
            }

            return resultString;
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

        private DateTime LastReportTime()
        {
            return _dayReports.Count == 0 ? DateTime.Today : _dayReports.Last().CreateTime;
        }

        public void CreateSprintReport(string name)
        {
            var resolvedTasks = FindTeamResolvedTasks();
            var sprintReport = new SprintReport(Id, resolvedTasks);
            sprintReport.CreateReport(name);
        }

        private List<Task> FindTeamResolvedTasks()
        {
            var result = new List<Task>();
            foreach (var task in from subordinate in _subordinates from task in subordinate.AllResolved() where !result.Contains(task) select task)
            {
                result.Add(task);
            }

            return result;
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

        public void UpdateTaskEmployee(string taskId, IEmployee assigned)
        {
            if (!IsMySubordinate(assigned)) throw new EmployeeUpdateException();
            TaskManagementSystem.TaskManagementSystem.UpdateTaskEmployee(this, taskId, assigned);
        }

        private bool IsMySubordinate(IEmployee assigned)
        {
            return _subordinates.Contains(assigned);
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
            {
                newDirector.AddNewSubordinate(subordinate);
            }
            else
            {
                throw new SubordinateException();
            }
        }

        public List<ISubordinate> Subordinates()
        {
            return _subordinates;
        }
    }
}