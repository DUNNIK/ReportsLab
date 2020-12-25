using System;
using System.Collections.Generic;
using System.Linq;
using ReportsLab.BusinessLogicLayer.TaskManagementSystem;

namespace ReportsLab.BusinessLogicLayer.ReportingSystem
{
    public abstract class Report
    {
        public readonly DateTime CreateTime = DateTime.Now;
        public List<Task> Tasks = new List<Task>();

        public List<Task> ReportTasks()
        {
            return Tasks;
        }

        public void CreateReport(string name)
        {
            var resultString = Tasks.Aggregate("", (current, task) => current + task.ToString());
            StorageLayer.Storage.CreateFile(name, resultString);
        }
    }
}