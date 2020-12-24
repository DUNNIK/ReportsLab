using System;

namespace ReportsLab.StorageLayer.Exceptions
{
    public class FileRemoveException : Exception
    {
        public FileRemoveException() : base("Unable to remove file!")
        {
        }
    }
}