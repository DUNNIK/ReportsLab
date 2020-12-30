using System;

namespace ReportsLab.Exceptions
{
    public class ManagerException : Exception
    {
        public ManagerException() : base("This item cannot be found")
        {
        }
    }
}