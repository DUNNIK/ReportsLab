using System;
using System.Collections.Generic;

namespace ReportsLab.BusinessLogicLayer.ReportingSystem
{
    public class DayReport : Report
    {
        public DayReport(string userId, DateTime lastReportTime)
        {
            _tasks = TaskManagementSystem.TaskManagementSystem.EmployeeTasksForPeriod(userId, lastReportTime);
            AddToReportsData(userId);
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