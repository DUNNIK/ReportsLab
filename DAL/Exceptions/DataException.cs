using System;

namespace DAL.Exceptions
{
    public class DataException : Exception
    {
        public DataException() : base("There is no such object")
        {
        }
    }
}