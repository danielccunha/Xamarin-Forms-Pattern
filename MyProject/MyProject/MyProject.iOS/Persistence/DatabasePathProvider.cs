using MyProject.Contracts.Persistence;
using System;
using System.IO;

namespace MyProject.iOS.Persistence
{
    public class DatabasePathProvider : IDatabasePathProvider
    {
        public string GetDatabasePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            path = Path.Combine(path, "..", "Library");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return Path.Combine(path, filename);
        }
    }
}