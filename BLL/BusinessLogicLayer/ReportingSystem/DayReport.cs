using System;
using System.Collections.Generic;
using DAL.StorageLayer.Report;

namespace ReportsLab.BusinessLogicLayer.ReportingSystem
{
    public class DayReport : Report
    {
        public DayReport(string userId, DateTime lastReportTime)
        {
            Tasks = TaskManagementSystem.TaskManagementSystem.EmployeeResolvedTasksForPeriod(userId, lastReportTime);
            AddToReportsData(userId);
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