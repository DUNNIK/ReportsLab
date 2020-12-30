using System.Linq;
using DAL.Entities.Task;
using ReportsLab.EmployeeSystem;
using static System.Console;

namespace UI
{
    public static class EmployeeMenu
    {
        private static void PrintEmployeeMenu() {
            WriteLine(
                "0 - Exit\n" +
                "1 - Show all tasks\n" +
                "2 - Create a new Task\n" +
                "3 - Create comment to Task\n" +
                "4 - Open Task\n" +
                "5 - Active Task\n" +
                "6 - Resolve Task\n" +
                "7 - Make a daily report\n" +
                "8 - Make a sprint report\n" +
                "9 - Update sprint report\n" +
                "10 - Close the current sprint report\n"
            );
        }
        public static void Call(Employee employee)
        {
            var exit = false;
            var command = 0;
            while (!exit)
            {
                PrintEmployeeMenu();
                try
                {
                    command = int.Parse(ReadLine());
                }
                catch
                {
                    WriteLine("Invalid command");
                }
                switch (command)
                {
                    case 0:
                        exit = true;
                        break;
                    case 1:
                        var result = TaskData.TasksById.Values.Aggregate("", (current, tasks) => current + tasks);
                        WriteLine(result);
                        break;
                    case 2:
                        WriteLine("What is the name of the task?");
                        var name = ReadLine();
                        WriteLine("What is the description of the task?");
                        var description = ReadLine();
                        var id = employee.CreateTask(name, description);
                        WriteLine($"New task id: {id}");
                        break;
                    case 3:
                        WriteLine("What is the id of the task?");
                        var taskId = ReadLine();
                        WriteLine("Write your comment:");
                        var comment = ReadLine();
                        employee.CreateCommit(taskId, comment);
                        break;
                    case 4:
                        WriteLine("What is the id of the task?");
                        taskId = ReadLine();
                        employee.OpenTask(taskId);
                        break;
                    case 5:
                        WriteLine("What is the id of the task?");
                        taskId = ReadLine();
                        employee.ActiveTask(taskId);
                        break;
                    case 6:
                        WriteLine("What is the id of the task?");
                        taskId = ReadLine();
                        employee.ResolveTask(taskId);
                        break;
                    case 7:
                        WriteLine("What is the name for the report?");
                        name = ReadLine();
                        employee.CreateDayReport(name);
                        break;
                    case 8:
                        WriteLine("What is the name for the report?");
                        name = ReadLine();
                        employee.CreateSprintReport(name);
                        break;
                    case 9:
                        employee.UpdateSprintReport();
                        break;
                    case 10:
                        employee.CloseCurrentSprintReport();
                        break;
                    default:
                        WriteLine("Wrong command!");
                        break;
                }
            }
        }
    }
}