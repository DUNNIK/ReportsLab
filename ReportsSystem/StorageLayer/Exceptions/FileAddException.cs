using System;

namespace ReportsLab.StorageLayer.Exceptions
{
    public class FileAddException : Exception
    {
        public FileAddException() : base("Unable to add file!")
        {
        }
    }
}