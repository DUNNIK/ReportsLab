using System;
using System.Collections.Generic;
using DAL.StorageLayer.Infrastructure;

namespace DAL.StorageLayer.Employee
{
    public class Employee : IEmployee
    {
        private readonly List<Report.Report> _dayReports = new List<Report.Report>();
        private readonly string _name;

        public Employee(string name)
        {
            _name = name;
        }

        public string Id { get; } = Guid.NewGuid().ToString();
    }
}