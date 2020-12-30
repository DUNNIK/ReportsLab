using System.Linq;
using DAL.Entities.Task;
using ReportsLab.EmployeeSystem;
using static System.Console;
namespace UI
{
    public static class DirectorMenu
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
                "10 - Assign a task to a subordinate\n" +
                "11 - Add a subordinate to the team\n" +
                "12 - My Subordinates list\n" +
                "13 - Close the current sprint report\n"
            );
        }

        public static void Call(Director director)
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
                        var id = director.CreateTask(name, description);
                        WriteLine($"New task id: {id}");
                        break;
                    case 3:
                        WriteLine("What is the id of the task?");
                        var taskId = ReadLine();
                        WriteLine("Write your comment:");
                        var comment = ReadLine();
                        director.CreateCommit(taskId, comment);
                        break;
                    case 4:
                        WriteLine("What is the id of the task?");
                        taskId = ReadLine();
                        director.OpenTask(taskId);
                        break;
                    case 5:
                        WriteLine("What is the id of the task?");
                        taskId = ReadLine();
                        director.ActiveTask(taskId);
                        break;
                    case 6:
                        WriteLine("What is the id of the task?");
                        taskId = ReadLine();
                        director.ResolveTask(taskId);
                        break;
                    case 7:
                        WriteLine("What is the name for the report?");
                        name = ReadLine();
                        director.CreateDayReport(name);
                        break;
                    case 8:
                        WriteLine("What is the name for the report?");
                        name = ReadLine();
                        director.CreateSprintReport(name);
                        break;
                    case 9:
                        director.UpdateSprintReport();
                        break;
                    case 10:
                        WriteLine("What is the id of the task?");
                        taskId = ReadLine();
                        var resultSubordinate = SearchSubordinate(director);
                        director.UpdateTaskEmployee(taskId, resultSubordinate);
                        break;
                    case 11:
                        resultSubordinate = SearchSubordinate(director);
                        director.AddNewSubordinate(resultSubordinate);
                        break;
                    case 12:
                        foreach (var subordinate in director.Subordinates())
                        {
                            WriteLine(subordinate.ToString());
                        }
                        break;
                    case 13:
                        director.CloseCurrentSprintReport();
                        break;
                    default:
                        WriteLine("Wrong command!");
                        break;
                }
            }
        }

        private static ISubordinate SearchSubordinate(IDirector director)
        {
            WriteLine("What is the id of your subordinate?");
            var subordinateId = ReadLine();
            ISubordinate resultSubordinate = null;
            foreach (var subordinate in director.Subordinates().Where(subordinate => subordinate.Id == subordinateId))
            {
                resultSubordinate = subordinate;
            }

            return resultSubordinate;
        }
    }
}