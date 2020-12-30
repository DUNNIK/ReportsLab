using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Infrastructure;

namespace DAL.Entities.Task
{
    public class Task
    {
        public enum State
        {
            Open,
            Active,
            Resolved
        }

        public readonly List<TaskMemento> Changes = new List<TaskMemento>();
        public readonly string Description;
        public readonly string Id = Guid.NewGuid().ToString();
        public readonly string Name;
        public IEmployee AssignedEmployee;
        public List<string> Comments = new List<string>();
        public int CurrentChange;
        public State CurrentState;

        public Task(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public override string ToString()
        {
            return $"{Name} task\nId: {Id}\nState: {CurrentState.ToString()}\n{Description}\n";
        }

        public DateTime LastChangeDateTime()
        {
            return Changes.Last().CreateTime;
        }

        public State Status()
        {
            return CurrentState;
        }
    }
}