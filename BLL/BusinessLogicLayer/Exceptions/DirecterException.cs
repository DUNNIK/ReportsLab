using System;

namespace ReportsLab.BusinessLogicLayer.Exceptions
{
    public class DirecterException : Exception
    {
        public DirecterException() : base("A subordinate cannot have more than one Director!")
        {
        }
    }
}