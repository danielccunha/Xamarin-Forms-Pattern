namespace MyProject.Contracts.Persistence
{
    public interface IDatabasePathProvider
    {
        string GetDatabasePath(string filename);
    }
}
