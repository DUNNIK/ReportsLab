using System;

namespace DAL.Exceptions
{
    public class FileRemoveException : Exception
    {
        public FileRemoveException() : base("Unable to remove file!")
        {
        }
    }
}