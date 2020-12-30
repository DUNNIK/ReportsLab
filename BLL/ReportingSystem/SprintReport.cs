using System.Collections.Generic;
using System.Linq;
using DAL.Entities.Report;
using DAL.Entities.Task;

namespace ReportsLab.ReportingSystem
{
    public class SprintReport : Report
    {
        public SprintReport(string userId, IEnumerable<DayReport> sprint)
        {
            Tasks = SelectAllTasksForSprint(sprint);
            AddToReportsData(userId);
        }

        public SprintReport(string userId, List<Task> resolvedTasks)
        {
            Tasks = resolvedTasks;
            AddToReportsData(userId);
        }

        public void Update(IEnumerable<DayReport> sprint)
        {
            Tasks = SelectAllTasksForSprint(sprint);
        }
        public void Update(List<Task> resolvedTasks)
        {
            Tasks = resolvedTasks;;
        }
        private List<Task> SelectAllTasksForSprint(IEnumerable<DayReport> sprint)
        {
            var result = new List<Task>();
            foreach (var task in from report in sprint
                from task in report.ReportTasks()
                where !result.Contains(task)
                select task)
                result.Add(task);

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
                ReportsData.EmployeeDayReports.Add(userId, new List<Report>());
        }
    }
}