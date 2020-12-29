using System;
using DAL.StorageLayer.Infrastructure;

namespace DAL.Entities.Employee
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