using System;

namespace UI
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                EmployeeSelection.Call();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}