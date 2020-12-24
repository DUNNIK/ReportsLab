using System;

namespace StorageLayer.Exceptions
{
    public class FileAddException : Exception
    {
        public FileAddException() : base("Unable to add file!")
        {
        }
    }
}