using System;
using DAL.Infrastructure;

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
        public override string ToString()
        {
            return Name + " - " + Id + "\n";
        }
    }
}