using System.Collections.Generic;
using System.Linq;
using ReportsLab.BusinessLogicLayer.TaskManagementSystem;

namespace ReportsLab.BusinessLogicLayer.ReportingSystem
{
    public class SprintReport : Report
    {
        public SprintReport(string userId, List<DayReport> sprint)
        {
            _tasks = SelectAllTasksForSprint(sprint);
            AddToReportsData(userId);
        }

        private List<Task> SelectAllTasksForSprint(List<DayReport> sprint)
        {
            var result = new List<Task>();
            foreach (var task in from report in sprint
                from task in report.ReportTasks()
                where !result.Contains(task)
                select task)
            {
                result.Add(task);
            }

            return result;
        }
        private void AddToReportsData(string userId)
        {
            CreateNewIfNotIdReportsData(userId);
            StorageLayer.ReportsData.EmployeeDayReports[userId].Add(this);
        }

        private void CreateNewIfNotIdReportsData(string userId)
        {
            if (!StorageLayer.ReportsData.EmployeeDayReports.ContainsKey(userId))
            {
                StorageLayer.ReportsData.EmployeeDayReports.Add(userId, new List<Report>());
            }
        }
    }
}