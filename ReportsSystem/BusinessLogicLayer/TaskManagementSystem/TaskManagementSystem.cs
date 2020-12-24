using System;
using System.Collections.Generic;
using ReportsLab.BusinessLogicLayer.EmployeeSystem;
using ReportsLab.StorageLayer;

namespace ReportsLab.BusinessLogicLayer.TaskManagementSystem
{
    public static class TaskManagementSystem
    {

        public static void OpenTask(IEmployee creator, string id)
        {
            AddToChangesDatabase(creator.Id, Task(id).Open());
        }
        public static void ActiveTask(IEmployee creator, string id)
        {
            AddToChangesDatabase(creator.Id, Task(id).Active());
        }
        public static void ResolveTask(IEmployee creator, string id)
        {
            AddToChangesDatabase(creator.Id, Task(id).Resolved());
        }

        public static void CreateCommit(IEmployee creator, string id, string comment)
        {
            AddToChangesDatabase(creator.Id, Task(id).AddComment(comment));
        }
        public static Task Task(string taskId)
        {
            return TaskData.TasksById[taskId];
        }

        public static List<Task> TasksEmployee(string employeeId)
        {
            return TaskData.TasksByEmployeeId[employeeId];
        }

        public static List<Task> TasksByChangeCreateTime(DateTime changesTime)
        {
            var result = new List<Task>();
            foreach (var employeeChanges in TaskData.ChangesByEmployeeId.Values)
            {
                foreach (var change in employeeChanges)
                {
                    if (change.CreateTime == changesTime && !result.Contains(change.TaskReference))
                    {
                        result.Add(change.TaskReference);
                    }
                }
            }

            return result;
        }
        
        public static List<Task> TasksByLastChangeTime(DateTime changesTime)
        {
            var result = new List<Task>();
            foreach (var task in TaskData.TasksById.Values)
            {
                if (changesTime == task.LastChangeDateTime())
                {
                    result.Add(task);
                }
            }

            return result;
        }
        
        public static List<Task> TasksEmployeeChanged(string employeeId)
        {
            var result = new List<Task>();
            foreach (var changes in TaskData.ChangesByEmployeeId[employeeId])
            {
                result.Add(changes.TaskReference);
            }

            return result;
        }

        public static List<Task> TasksAssignedToSubordinates(IDirector director)
        {
            var result = new List<Task>();
            foreach (var subordinate in director.Subordinates())
            {
                foreach (var task in TasksEmployee(subordinate.Id))
                {
                    result.Add(task);
                }
            }

            return result;
        }

        public static List<Task> EmployeeTasksForPeriod(string employeeId, DateTime lowTime)
        {
            var result = new List<Task>();
            foreach (var employeeChanges in TaskData.ChangesByEmployeeId.Values)
            {
                foreach (var change in employeeChanges)
                {
                    if (change.CreateTime > lowTime && !result.Contains(change.TaskReference))
                    {
                        result.Add(change.TaskReference);
                    }
                }
            }

            return result;
        }
        public static string CreateTask(IEmployee creator, string name, string description)
        {
            var newTask = new Task(name, description);
            AddToTaskByIdDatabase(newTask.Id, newTask);
            return newTask.Id;
        }

        public static void UpdateTaskEmployee(IEmployee creator, string taskId, IEmployee assigned)
        {
            AddToChangesDatabase(creator.Id, Task(taskId).AssignEmployee(assigned));
            AddToTaskByEmployeeIdDatabase(assigned.Id, Task(taskId));
        }
        
        private static void AddToTaskByIdDatabase(string taskId, Task task)
        {
            TaskData.TasksById.Add(taskId, task);
        }
        private static void AddToChangesDatabase(string employeeId, TaskMemento memento)
        {
            CreateNewIfNotIdChanges(employeeId);
            TaskData.ChangesByEmployeeId[employeeId].Add(memento);
        }
        private static void AddToTaskByEmployeeIdDatabase(string employeeId, Task task)
        {
            CreateNewIfNotIdTasksByEmployeeId(employeeId);
            TaskData.TasksByEmployeeId[employeeId].Add(task);
        }

        private static void CreateNewIfNotIdTasksByEmployeeId(string id)
        {
            if (!TaskData.TasksByEmployeeId.ContainsKey(id))
            {
                TaskData.TasksByEmployeeId.Add(id, new List<Task>());
            }
        }
        private static void CreateNewIfNotIdChanges(string id)
        {
            if (!TaskData.ChangesByEmployeeId.ContainsKey(id))
            {
                TaskData.ChangesByEmployeeId.Add(id, new List<TaskMemento>());
            }
        }
    }
}