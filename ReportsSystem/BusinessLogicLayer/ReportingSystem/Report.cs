using System;
using System.Collections.Generic;
using System.Linq;
using ReportsLab.BusinessLogicLayer.TaskManagementSystem;

namespace ReportsLab.BusinessLogicLayer.ReportingSystem
{
    public abstract class Report
    {
        public readonly DateTime CreateTime = DateTime.Now;
        protected List<Task> _tasks = new List<Task>();

        public List<Task> ReportTasks()
        {
            return _tasks;
        }

        public void CreateReport(string name)
        {
            var resultString = _tasks.Aggregate("", (current, task) => current + task.ToString());
            StorageLayer.Storage.CreateFile(name, resultString);
        }
    }
}