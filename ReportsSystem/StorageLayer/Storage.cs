﻿using System;
using System.IO;
using ReportsLab.StorageLayer.Exceptions;

namespace ReportsLab.StorageLayer
{
    public static class Storage
    {
        private const string FolderPath = @"D:\ООП";

        public static void CreateFile(string fileName, string data)
        {
            fileName = @$"{fileName}";
            var filePath = Path.Combine(FolderPath, fileName);
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.Write(data);
            }
        }
        
    }
}