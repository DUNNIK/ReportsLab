using System;
using System.Linq;
using ReportsLab.BusinessLogicLayer.EmployeeSystem;

namespace ReportsLab.BusinessLogicLayer.TaskManagementSystem
{
    public class Task
    {
        public string Id { get; }
        public DAL.StorageLayer.Task.Task _task;
        public Task(string name, string description)
        {
            _task = new DAL.StorageLayer.Task.Task(name, description);
            Id = _task.Id;
            ChangesUpdate();
        }
        public DAL.StorageLayer.Task.TaskMemento AddComment(string comment)
        {
            _task.Comments.Add(comment);
            ChangesUpdate();
            return CreateNewMemento();
        }

        public DAL.StorageLayer.Task.TaskMemento AssignEmployee(IEmployee employee)
        {
            _task.AssignedEmployee = employee.EmployeeInfo;
            ChangesUpdate();
            return CreateNewMemento();
        }
        
        private void ChangesUpdate()
        {
            _task.Changes.Add(CreateNewMemento());
            _task.CurrentChange++;
        }

        public void Restore(DAL.StorageLayer.Task.TaskMemento memento)
        {
            if (memento == null) return;
            RestoreInfoFromMemento(memento);
            _task.Changes.Add(memento);
            _task.CurrentChange = _task.Changes.Count - 1;
        }

        private void RestoreInfoFromMemento(DAL.StorageLayer.Task.TaskMemento memento)
        {
            _task.CurrentState = memento.State;
            _task.AssignedEmployee = memento.Employee;
            _task.Comments = memento.Comments;
        }
        public DAL.StorageLayer.Task.TaskMemento Undo()
        {
            if (_task.CurrentChange <= 0) return null;
            var memento = _task.Changes[--_task.CurrentChange];
            RestoreInfoFromMemento(memento);
            return memento;
        }

        public DAL.StorageLayer.Task.TaskMemento Redo()
        {
            if (_task.CurrentChange + 1 < _task.Changes.Count)
            {
                var memento = _task.Changes[++_task.CurrentChange];
                RestoreInfoFromMemento(memento);
                return memento;
            }

            return null;
        }
        private DAL.StorageLayer.Task.TaskMemento CreateNewMemento()
        {
            return new DAL.StorageLayer.Task.TaskMemento(_task.CurrentState, _task.AssignedEmployee, _task.Comments, _task);
        }

        public DAL.StorageLayer.Task.TaskMemento Open()
        {
            if (_task.CurrentState == DAL.StorageLayer.Task.Task.State.Active || _task.CurrentState == DAL.StorageLayer.Task.Task.State.Resolved) return null;
            _task.CurrentState = DAL.StorageLayer.Task.Task.State.Open;
            
            return CreateNewMemento();
        }

        public DAL.StorageLayer.Task.TaskMemento Active()
        {
            if (_task.CurrentState == DAL.StorageLayer.Task.Task.State.Open)
            {
                _task.CurrentState = DAL.StorageLayer.Task.Task.State.Active;
            }

            return CreateNewMemento();
        }

        public DAL.StorageLayer.Task.TaskMemento Resolved()
        {
            if (_task.CurrentState == DAL.StorageLayer.Task.Task.State.Active)
            {
                _task.CurrentState = DAL.StorageLayer.Task.Task.State.Resolved;
            }

            return CreateNewMemento();
        }

        public override string ToString()
        {
            return $"{_task.Name} task\nState: {_task.CurrentState.ToString()}\n{_task.Description}\n";
        }

        public DateTime LastChangeDateTime()
        {
            return _task.Changes.Last().CreateTime;
        }

        public DAL.StorageLayer.Task.Task.State Status()
        {
            return _task.CurrentState;
        }
    }
}