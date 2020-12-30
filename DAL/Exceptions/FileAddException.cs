using System;

namespace DAL.Exceptions
{
    public class FileAddException : Exception
    {
        public FileAddException() : base("Unable to add file!")
        {
        }
    }
}