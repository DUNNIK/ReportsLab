using System.Collections.Generic;
using System.IO;
using ReportsLab.StorageLayer.Exceptions;

namespace StorageLayer.Storage.FolderClass
{
    internal class FolderBuilder : IStorageCreator
    {
        private readonly Folder _folder;

        public FolderBuilder() => _folder = new Folder();
        public FolderBuilder(string path)
        {
            _folder = new Folder(path);
        }

        public void Create(List<string> files)
        {
            try
            {
                CreateFiles(files);
            }
            catch
            {
                throw new FileAddException();
            }
        }

        private void CreateFiles(List<string> filePaths)
        {
            foreach (var filePath in filePaths)
            {
                CreateOneFile(filePath);
            }
        }

        private void CreateOneFile(string filePath)
        {
            File.Copy(filePath, 
                System.IO.Path
                    .Combine(_folder.FolderPath, System.IO.Path.GetFileName(filePath)), true);
            var file = new OrdinaryFile(filePath);
            _folder.AddFolderFile(file);
        }
        public void AddFileTo(string filePath)
        {
            try
            {
                CreateOneFile(filePath);
            }
            catch
            {
                throw new FileAddException();
            }
        }

        public void RemoveFileFrom(string filePath)
        {
            try
            {
                File.Delete(System.IO.Path.
                    Combine(_folder.FolderPath, System.IO.Path.GetFileName(filePath)));
            }
            catch
            {
                throw new FileRemoveException();
            }
        }

        public void RemoveAll()
        {
            try
            {
                Directory.Delete(_folder.FolderPath, true);
            }
            catch
            {
                throw new FileRemoveException();
            }

        }
        public string Path()
        {
            return _folder.FolderPath;
        }

        public IStorageComponent Build() => _folder;
    }
}