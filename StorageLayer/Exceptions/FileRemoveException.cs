using System;

namespace StorageLayer.Exceptions
{
    public class FileRemoveException : Exception
    {
        public FileRemoveException() : base("Unable to remove file!")
        {
        }
    }
}