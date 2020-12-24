using System;

namespace ReportsLab.BusinessLogicLayer.Exceptions
{
    public class SubordinateException : Exception
    {
        public SubordinateException() : base("Subordinate unavailable!")
        {
        }
    }
}