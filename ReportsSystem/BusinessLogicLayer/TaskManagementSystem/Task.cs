using System;
using System.Collections.Generic;
using System.Linq;
using ReportsLab.BusinessLogicLayer.EmployeeSystem;
using IEmployee = DAL.StorageLayer.Infrastructure.IEmployee;

namespace ReportsLab.BusinessLogicLayer.TaskManagementSystem
{
    public class Task
    {
        private DAL.StorageLayer.Task.Task _task;
        public Task(string name, string description)
        {
            _task = new DAL.StorageLayer.Task.Task(name, description);
            ChangesUpdate();
        }
        public TaskMemento AddComment(string comment)
        {
            _task._comments.Add(comment);
            ChangesUpdate();
            return CreateNewMemento();
        }

        public TaskMemento AssignEmployee(IEmployee employee)
        {
            _task._assignedEmployee = employee;
            ChangesUpdate();
            return CreateNewMemento();
        }
        
        private void ChangesUpdate()
        {
            _changes.Add(CreateNewMemento());
            _currentChange++;
        }

        public void Restore(TaskMemento memento)
        {
            if (memento == null) return;
            RestoreInfoFromMemento(memento);
            _changes.Add(memento);
            _currentChange = _changes.Count - 1;
        }

        private void RestoreInfoFromMemento(TaskMemento memento)
        {
            _currentState = memento.State;
            _assignedEmployee = memento.Employee;
            _comments = memento.Comments;
        }
        public TaskMemento Undo()
        {
            if (_currentChange <= 0) return null;
            var memento = _changes[--_currentChange];
            RestoreInfoFromMemento(memento);
            return memento;
        }

        public TaskMemento Redo()
        {
            if (_currentChange + 1 < _changes.Count)
            {
                var memento = _changes[++_currentChange];
                RestoreInfoFromMemento(memento);
                return memento;
            }

            return null;
        }
        private TaskMemento CreateNewMemento()
        {
            return new TaskMemento(_currentState, _assignedEmployee, _comments, this);
        }

        public TaskMemento Open()
        {
            if (_currentState == State.Active || _currentState == State.Resolved) return null;
            _currentState = State.Open;
            
            return CreateNewMemento();
        }

        public TaskMemento Active()
        {
            if (_currentState == State.Open)
            {
                _currentState = State.Active;
            }

            return CreateNewMemento();
        }

        public TaskMemento Resolved()
        {
            if (_currentState == State.Active)
            {
                _currentState = State.Resolved;
            }

            return CreateNewMemento();
        }

        public override string ToString()
        {
            return $"{Name} task\nState: {_currentState.ToString()}\n{Description}\n";
        }

        public DateTime LastChangeDateTime()
        {
            return _changes.Last().CreateTime;
        }

        public State Status()
        {
            return _currentState;
        }
    }
}