using MyProject.Contracts.Persistence;
using System;
using System.IO;

namespace MyProject.Droid.Persistence
{
    public class DatabasePathProvider : IDatabasePathProvider
    {
        public string GetDatabasePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}