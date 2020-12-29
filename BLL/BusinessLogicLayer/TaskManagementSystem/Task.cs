using System;
using System.Linq;
using DAL.StorageLayer.Task;
using ReportsLab.BusinessLogicLayer.EmployeeSystem;

namespace ReportsLab.BusinessLogicLayer.TaskManagementSystem
{
    public class Task
    {
        public DAL.StorageLayer.Task.Task _task;

        public Task(string name, string description)
        {
            _task = new DAL.StorageLayer.Task.Task(name, description);
            Id = _task.Id;
            ChangesUpdate();
        }

        public string Id { get; }

        public TaskMemento AddComment(string comment)
        {
            _task.Comments.Add(comment);
            ChangesUpdate();
            return CreateNewMemento();
        }

        public TaskMemento AssignEmployee(IEmployee employee)
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

        public void Restore(TaskMemento memento)
        {
            if (memento == null) return;
            RestoreInfoFromMemento(memento);
            _task.Changes.Add(memento);
            _task.CurrentChange = _task.Changes.Count - 1;
        }

        private void RestoreInfoFromMemento(TaskMemento memento)
        {
            _task.CurrentState = memento.State;
            _task.AssignedEmployee = memento.Employee;
            _task.Comments = memento.Comments;
        }

        public TaskMemento Undo()
        {
            if (_task.CurrentChange <= 0) return null;
            var memento = _task.Changes[--_task.CurrentChange];
            RestoreInfoFromMemento(memento);
            return memento;
        }

        public TaskMemento Redo()
        {
            if (_task.CurrentChange + 1 < _task.Changes.Count)
            {
                var memento = _task.Changes[++_task.CurrentChange];
                RestoreInfoFromMemento(memento);
                return memento;
            }

            return null;
        }

        private TaskMemento CreateNewMemento()
        {
            return new TaskMemento(_task.CurrentState, _task.AssignedEmployee, _task.Comments, _task);
        }

        public TaskMemento Open()
        {
            if (_task.CurrentState == DAL.StorageLayer.Task.Task.State.Active ||
                _task.CurrentState == DAL.StorageLayer.Task.Task.State.Resolved) return null;
            _task.CurrentState = DAL.StorageLayer.Task.Task.State.Open;

            return CreateNewMemento();
        }

        public TaskMemento Active()
        {
            if (_task.CurrentState == DAL.StorageLayer.Task.Task.State.Open)
                _task.CurrentState = DAL.StorageLayer.Task.Task.State.Active;

            return CreateNewMemento();
        }

        public TaskMemento Resolved()
        {
            if (_task.CurrentState == DAL.StorageLayer.Task.Task.State.Active)
                _task.CurrentState = DAL.StorageLayer.Task.Task.State.Resolved;

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