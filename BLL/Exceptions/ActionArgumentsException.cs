using System;

namespace ReportsLab.Exceptions
{
    public class ActionArgumentsException : Exception
    {
        public ActionArgumentsException() : base("Invalid Arguments!")
        {
        }
    }
}