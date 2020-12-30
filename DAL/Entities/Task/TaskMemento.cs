using System;
using System.Collections.Generic;
using DAL.Infrastructure;

namespace DAL.Entities.Task
{
    public class TaskMemento
    {
        public List<string> Comments;
        public DateTime CreateTime = DateTime.Now;
        public IEmployee Employee;
        public Entities.Task.Task.State State;
        public Entities.Task.Task TaskReference;

        public TaskMemento(Entities.Task.Task.State state, IEmployee employee, List<string> comments, Entities.Task.Task taskReference)
        {
            State = state;
            Employee = employee;
            Comments = comments;
            TaskReference = taskReference;
        }
    }
}