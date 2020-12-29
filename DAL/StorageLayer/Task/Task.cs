using System;
using System.Collections.Generic;
using DAL.StorageLayer.Infrastructure;

namespace DAL.StorageLayer.Task
{
    public class Task
    {
        public readonly string Id = Guid.NewGuid().ToString();
        public readonly string Name;
        public readonly string Description;
        public IEmployee _assignedEmployee;
        private State _currentState;
        public List<string> _comments = new List<string>();
        private readonly List<TaskMemento> _changes = new List<TaskMemento>();
        private int _currentChange;
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
            return $"{Name} task\nState: {_currentState.ToString()}\n{Description}\n";
        }
    }
}