using DAL.Entities.Employee;
using ReportsLab.EmployeeSystem;
using static System.Console;

namespace UI
{
    public static class AllEmployeesMenu
    {
        private static void PrintAllEmployeeMenu()
        {
            WriteLine(
                "0 - Exit\n" +
                "1 - List of all employees\n" +
                "2 - List of all ordinary employees\n" +
                "3 - List of all directors\n" +
                "4 - List of all team leads\n"
            );
        }

        public static void Call()
        {
            var exit = false;
            var command = 0;
            while (!exit)
            {
                PrintAllEmployeeMenu();
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
                        foreach (var employee in EmployeeData.AllEmployees.Values)
                        {
                            WriteLine(employee.ToString());
                        }
                        break;
                    case 2:
                        foreach (var employee in EmployeesManager.AllOrdinaryEmployees.Values)
                        {
                            WriteLine(employee.ToString());
                        }
                        break;
                    case 3:
                        foreach (var employee in EmployeesManager.AllDirectors.Values)
                        {
                            WriteLine(employee.ToString());
                        }
                        break;
                    case 4:
                        foreach (var employee in EmployeesManager.AllTeamLeads.Values)
                        {
                            WriteLine(employee.ToString());
                        }
                        break;

                }

            }
        }
    }
}