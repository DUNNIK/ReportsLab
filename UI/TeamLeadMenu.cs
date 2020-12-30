using System.Linq;
using DAL.Entities.Task;
using ReportsLab.EmployeeSystem;
using ReportsLab.TaskManagementSystem;
using static System.Console;

namespace UI
{
    public static class TeamLeadMenu
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
                "12 - My Subordinates list\n"
            );
        }

        public static void Call(TeamLead teamLead)
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
                        foreach (var task in TaskData.TasksById.Values)
                        {
                            WriteLine(task.ToString());
                        }
                        break;
                    case 2:
                        WriteLine("What is the name of the task?");
                        var name = ReadLine();
                        WriteLine("What is the description of the task?");
                        var description = ReadLine();
                        var id = teamLead.CreateTask(name, description);
                        WriteLine($"New task id: {id}");
                        break;
                    case 3:
                        WriteLine("What is the id of the task?");
                        var taskId = ReadLine();
                        WriteLine("Write your comment:");
                        var comment = ReadLine();
                        teamLead.CreateCommit(taskId, comment);
                        break;
                    case 4:
                        WriteLine("What is the id of the task?");
                        taskId = ReadLine();
                        teamLead.OpenTask(taskId);
                        break;
                    case 5:
                        WriteLine("What is the id of the task?");
                        taskId = ReadLine();
                        teamLead.ActiveTask(taskId);
                        break;
                    case 6:
                        WriteLine("What is the id of the task?");
                        taskId = ReadLine();
                        teamLead.ResolveTask(taskId);
                        break;
                    case 7:
                        WriteLine("What is the name for the report?");
                        name = ReadLine();
                        teamLead.CreateDayReport(name);
                        break;
                    case 8:
                        WriteLine("What is the name for the report?");
                        name = ReadLine();
                        teamLead.CreateSprintReport(name);
                        break;
                    case 9:
                        teamLead.UpdateSprintReport();
                        break;
                    case 10:
                        WriteLine("What is the id of the task?");
                        taskId = ReadLine();
                        var resultSubordinate = SearchSubordinate(teamLead);
                        teamLead.UpdateTaskEmployee(taskId, resultSubordinate);
                        break;
                    case 11:
                        resultSubordinate = SearchSubordinate(teamLead);
                        teamLead.AddNewSubordinate(resultSubordinate);
                        break;
                    case 12:
                        foreach (var subordinate in teamLead.Subordinates())
                        {
                            WriteLine(subordinate.ToString());
                        }
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