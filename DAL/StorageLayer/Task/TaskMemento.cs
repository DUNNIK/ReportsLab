using System;
using System.Collections.Generic;
using DAL.StorageLayer.Infrastructure;

namespace DAL.StorageLayer.Task
{
    public class TaskMemento
    {
        public DateTime CreateTime = DateTime.Now;
        public Task.State State;
        public IEmployee Employee;
        public List<string> Comments;
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