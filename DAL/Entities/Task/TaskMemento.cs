using System;
using System.Collections.Generic;
using DAL.StorageLayer.Infrastructure;

namespace DAL.StorageLayer.Task
{
    public class TaskMemento
    {
        public List<string> Comments;
        public DateTime CreateTime = DateTime.Now;
        public IEmployee Employee;
        public Task.State State;
        public Task TaskReference;

        public TaskMemento(Task.State state, IEmployee employee, List<string> comments, Task taskReference)
        {
            State = state;
            Employee = employee;
            Comments = comments;
            TaskReference = taskReference;
        }
    }
}