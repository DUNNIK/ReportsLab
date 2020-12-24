using System.Collections.Generic;

namespace StorageLayer.Storage
{
    public interface IStorageCreator
    {
        void Create(List<string> files);
        void AddFileTo(string filePath);
        void RemoveFileFrom(string filePath);
        void RemoveAll();
        string Path();
        IStorageComponent Build();
    }
}