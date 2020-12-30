using System.Collections.Generic;
using DAL.Entities.Employee;
using ReportsLab.EmployeeSystem;
using ReportsLab.Exceptions;
using static System.Console;
using Employee = ReportsLab.EmployeeSystem.Employee;

namespace UI
{
    public static class EmployeeSelection
    {
        private static void PrintCreateEmployeeMenu() {
            WriteLine(
                "0 - Exit\n" +
                "1 - Create a new employee\n" +
                "2 - Create a new Director\n" +
                "3 - Create a new Team leader\n" +
                "4 - Go under existing employees\n" +
                "5 - Log in under an existing director\n" +
                "6 - Log in under an existing team lead\n" +
                "7 - List of all employees\n" +
                "8 - Menu of all employees\n"
            );
        }
        public static void Call()
        {
            var exit = false;
            var command = 0;
            while (!exit)
            {
                PrintCreateEmployeeMenu();
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
                        WriteLine("Write your name:");
                        var name = ReadLine();
                        var director = DirectorSearch();
                        if (name == null || director == null) break;
                        var employee = new Employee(name, director);
                        EmployeeMenu.Call(employee);
                        break;
                    case 2:
                        WriteLine("Write your name:");
                        name = ReadLine();
                        director = DirectorSearch();
                        var subordinates = SubordinatesSearch();
                        if (name == null || director == null) break;
                        var dir = new Director(name, director, subordinates);
                        DirectorMenu.Call(dir);
                        break;
                    case 3:
                        WriteLine("Write your name:");
                        name = ReadLine();
                        subordinates = SubordinatesSearch();
                        if (name == null) break;
                        var teamLead = new TeamLead(name, subordinates);
                        TeamLeadMenu.Call(teamLead);
                        break;
                    case 4:
                        WriteLine("Write Employee Id:");
                        var employeeId = ReadLine();
                        if (EmployeesManager.AllOrdinaryEmployees.ContainsKey(employeeId))
                        {
                            employee = EmployeesManager.AllOrdinaryEmployees[employeeId];
                        }
                        else
                        {
                            throw new ManagerException();
                        }
                        EmployeeMenu.Call(employee);
                        break;
                    case 5:
                        WriteLine("Write Director Id:");
                        var directorId = ReadLine();
                        if (EmployeesManager.AllDirectors.ContainsKey(directorId))
                        {
                            dir = EmployeesManager.AllDirectors[directorId];
                        }
                        else
                        {
                            throw new ManagerException();
                        }
                        DirectorMenu.Call(dir);
                        break;
                    case 6:
                        WriteLine("Write Team Lead Id:");
                        var teamLeadId = ReadLine();
                        if (EmployeesManager.AllTeamLeads.ContainsKey(teamLeadId))
                        {
                            teamLead = EmployeesManager.AllTeamLeads[teamLeadId];
                        }
                        else
                        {
                            throw new ManagerException();
                        }
                        TeamLeadMenu.Call(teamLead);
                        break;
                    case 7:
                        foreach (var employeesValue in EmployeeData.AllEmployees.Values)
                        {
                            WriteLine(employeesValue.ToString());
                        }
                        break;
                    case 8:
                        AllEmployeesMenu.Call();
                        break;
                }
            }
        }

        private static List<ISubordinate> SubordinatesSearch()
        {
            int command = 0;
            bool exit = false;
            var subordinates = new List<ISubordinate>();
            while (!exit)
            {
                WriteLine("0 - Exit\n" +
                          "1 - AddSubordinate\n"
                );
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
                        WriteLine("What's your subordinates id?");
                        var subordinateId = ReadLine();
                        try
                        {
                            if (EmployeesManager.AllDirectors.ContainsKey(subordinateId))
                            {
                                subordinates.Add(EmployeesManager.AllDirectors[subordinateId]);
                            } 
                            else if (EmployeesManager.AllOrdinaryEmployees.ContainsKey(subordinateId))
                            {
                                subordinates.Add(EmployeesManager.AllOrdinaryEmployees[subordinateId]);
                            }
                        }
                        catch (ManagerException)
                        {
                            throw new ManagerException();
                        }
                        break;
                    
                }
            }

            return subordinates;
        }

        private static IDirector DirectorSearch()
        {
            WriteLine("What's your director id?");
            var directorId = ReadLine();
            
            IDirector director = null;
            try
            {
                if (EmployeesManager.AllDirectors.ContainsKey(directorId))
                {
                    director = EmployeesManager.AllDirectors[directorId];
                }
                else if (EmployeesManager.AllTeamLeads.ContainsKey(directorId))
                {
                    director = EmployeesManager.AllTeamLeads[directorId];
                }
            }
            catch (ManagerException)
            {
                throw new ManagerException();
            }

            return director;
        }
    }
}