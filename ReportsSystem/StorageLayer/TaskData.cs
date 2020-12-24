﻿using System.Collections.Generic;
using ReportsLab.BusinessLogicLayer.TaskManagementSystem;

namespace ReportsLab.StorageLayer
{
    public static class TaskData
    {
        public static readonly Dictionary<string, List<Task>> TasksByEmployeeId = new Dictionary<string, List<Task>>();
        public static readonly Dictionary<string, Task> TasksById = new Dictionary<string, Task>();
        public static readonly Dictionary<string, List<TaskMemento>> ChangesByEmployeeId = new Dictionary<string, List<TaskMemento>>();
    }
}