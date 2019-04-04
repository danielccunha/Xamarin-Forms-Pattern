using SQLite;

namespace MyProject.Constants
{
    public static class PersistenceConstants
    {
        public const string DatabaseFilename = "database.db3";
        public const SQLiteOpenFlags DatabaseFlags = SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.FullMutex;
    }
}
