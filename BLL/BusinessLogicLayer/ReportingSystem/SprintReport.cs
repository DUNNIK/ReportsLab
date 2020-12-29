using System.Collections.Generic;
using System.Linq;
using DAL.StorageLayer.Report;
using ReportsLab.BusinessLogicLayer.TaskManagementSystem;

namespace ReportsLab.BusinessLogicLayer.ReportingSystem
{
    public class SprintReport : Report
    {
        public SprintReport(string userId, List<DayReport> sprint)
        {
            Tasks = SelectAllTasksForSprint(sprint);
            AddToReportsData(userId);
        }

        public SprintReport(string userId, List<DAL.StorageLayer.Task.Task> resolvedTasks)
        {
            Tasks = resolvedTasks;
            AddToReportsData(userId);
        }
        private List<DAL.StorageLayer.Task.Task> SelectAllTasksForSprint(List<DayReport> sprint)
        {
            var result = new List<DAL.StorageLayer.Task.Task>();
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
            ReportsData.EmployeeDayReports[userId].Add(this);
        }

        private void CreateNewIfNotIdReportsData(string userId)
        {
            if (!ReportsData.EmployeeDayReports.ContainsKey(userId))
            {
                ReportsData.EmployeeDayReports.Add(userId, new List<Report>());
            }
        }
    }
}