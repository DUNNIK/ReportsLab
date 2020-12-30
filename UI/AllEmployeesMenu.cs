using static System.Console;

namespace UI
{
    public static class AllEmployeesMenu
    {
        private static void PrintCreateEmployeeMenu()
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
                        
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;

                }

            }
        }
    }
}