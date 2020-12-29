using System;
using System.Collections.Generic;
using System.Linq;
using DAL.StorageLayer.Infrastructure;

namespace DAL.StorageLayer.Task
{
    public class Task
    {
        public readonly string Id = Guid.NewGuid().ToString();
        public readonly string Name;
        public readonly string Description;
        public IEmployee AssignedEmployee;
        public State CurrentState;
        public List<string> Comments = new List<string>();
        public readonly List<TaskMemento> Changes = new List<TaskMemento>();
        public int CurrentChange;
        public enum State
        {
            Open,
            Active,
            Resolved
        }
        public Task(string name, string description)
        {
            Name = name;
            Description = description;
        }
        
        public override string ToString()
        {
            return $"{Name} task\nState: {CurrentState.ToString()}\n{Description}\n";
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