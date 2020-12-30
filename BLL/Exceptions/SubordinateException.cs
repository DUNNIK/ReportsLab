using System;

namespace ReportsLab.Exceptions
{
    public class SubordinateException : Exception
    {
        public SubordinateException() : base("Subordinate unavailable!")
        {
        }
    }
}