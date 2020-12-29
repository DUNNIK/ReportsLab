using System;
using System.Collections.Generic;
using DAL.StorageLayer.Infrastructure;

namespace DAL.StorageLayer.Employee
{
    public class Employee : IEmployee
    {

        public Employee(string name)
        {
            Name = name;
        }

        public string Id { get; } = Guid.NewGuid().ToString();
        public string Name { get; }
    }
}