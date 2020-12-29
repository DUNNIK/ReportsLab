using System;

namespace DAL.StorageLayer.Exceptions
{
    public class FileAddException : Exception
    {
        public FileAddException() : base("Unable to add file!")
        {
        }
    }
}