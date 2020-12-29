using System;
using System.Collections.Generic;
using System.Linq;
using DAL.StorageLayer.Task;
using ReportsLab.BusinessLogicLayer.EmployeeSystem;

namespace ReportsLab.BusinessLogicLayer.TaskManagementSystem
{
    public static class TaskManagementSystem
    {
        public static readonly Dictionary<string, List<Task>> TasksByEmployeeId = new Dictionary<string, List<Task>>();
        public static readonly Dictionary<string, Task> TasksById = new Dictionary<string, Task>();
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
            return TasksById[taskId];
        }
        public static List<DAL.StorageLayer.Task.Task> TasksEmployee(string employeeId)
        {
            return TasksByEmployeeId[employeeId].Select(task => task._task).ToList();
        }

        public static List<DAL.StorageLayer.Task.Task> TasksByChangeCreateTime(DateTime changesTime)
        {
            var result = new List<DAL.StorageLayer.Task.Task>();
            foreach (var change in from employeeChanges in TaskData.ChangesByEmployeeId.Values from change in employeeChanges where change.CreateTime == changesTime && !result.Contains(change.TaskReference) select change)
            {
                result.Add(change.TaskReference);
            }

            return result;
        }
        
        public static List<DAL.StorageLayer.Task.Task> TasksByLastChangeTime(DateTime changesTime)
        {
            return TaskData.TasksById.Values.Where(task => changesTime == task.LastChangeDateTime()).ToList();
        }
        
        public static List<DAL.StorageLayer.Task.Task> TasksEmployeeChanged(string employeeId)
        {
            return TaskData.ChangesByEmployeeId[employeeId].Select(changes => changes.TaskReference).ToList();
        }

        public static List<DAL.StorageLayer.Task.Task> TasksAssignedToSubordinates(IDirector director)
        {
            return director.Subordinates().SelectMany(subordinate => TasksEmployee(subordinate.Id)).ToList();
        }

        public static List<DAL.StorageLayer.Task.Task> EmployeeResolvedTasksForPeriod(string employeeId, DateTime lowTime)
        {
            var result = new List<DAL.StorageLayer.Task.Task>();
            foreach (var change in TaskData.ChangesByEmployeeId[employeeId].Where(change => change.CreateTime > lowTime && !result.Contains(change.TaskReference) &&
                change.State == DAL.StorageLayer.Task.Task.State.Resolved))
                result.Add(change.TaskReference);

            return result;
        }
        public static string CreateTask(string name, string description)
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
            TasksById.Add(taskId, task);
        }
        private static void AddToChangesDatabase(string employeeId, TaskMemento memento)
        {
            CreateNewIfNotIdChanges(employeeId);
            TaskData.ChangesByEmployeeId[employeeId].Add(memento);
        }
        private static void AddToTaskByEmployeeIdDatabase(string employeeId, Task task)
        {
            CreateNewIfNotIdTasksByEmployeeId(employeeId);
            TasksByEmployeeId[employeeId].Add(task);
        }

        private static void CreateNewIfNotIdTasksByEmployeeId(string id)
        {
            if (!TaskData.TasksByEmployeeId.ContainsKey(id))
            {
                TasksByEmployeeId.Add(id, new List<Task>());
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