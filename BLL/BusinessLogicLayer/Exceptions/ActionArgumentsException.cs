using System;

namespace ReportsLab.BusinessLogicLayer.Exceptions
{
    public class ActionArgumentsException : Exception
    {
        public ActionArgumentsException() : base("Invalid Arguments!")
        {
        }
    }
}