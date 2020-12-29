using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.StorageLayer.Report
{
    public abstract class Report
    {
        public readonly DateTime CreateTime = DateTime.Now;
        public List<Task.Task> Tasks = new List<Task.Task>();
        
        public List<Task.Task> ReportTasks()
        {
            return Tasks;
        }

        public void CreateReport(string name)
        {
            var resultString = Tasks.Aggregate("", (current, task) => current + task);
            Storage.CreateFile(name, resultString);
        }
    }
}