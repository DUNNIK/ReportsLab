using System.Collections.Generic;

namespace DAL.Entities.Task
{
    public static class TaskData
    {
        public static readonly Dictionary<string, Task> TasksById = new Dictionary<string, Task>();

        public static readonly Dictionary<string, List<TaskMemento>> ChangesByEmployeeId =
            new Dictionary<string, List<TaskMemento>>();
    }
}