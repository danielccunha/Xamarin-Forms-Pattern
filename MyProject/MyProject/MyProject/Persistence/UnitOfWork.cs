using MyProject.Constants;
using MyProject.Contracts.Persistence;
using MyProject.Contracts.Persistence.Repositories;
using MyProject.Persistence.Repositories;
using SQLite;

namespace MyProject.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly string _databasePath;
        private readonly SQLiteAsyncConnection _conn;

        public UnitOfWork(IDatabasePathProvider provider)
        {
            _databasePath = provider.GetDatabasePath(PersistenceConstants.DatabaseFilename);
            _conn = new SQLiteAsyncConnection(_databasePath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.FullMutex);

            InitializeRepositories();
        }

        public IProductRepository Products { get; set; }

        private void InitializeRepositories()
        {
            Products = new ProductRepository(_conn);
        }

        public async void Dispose()
        {
            await _conn.CloseAsync();
            _conn.GetConnection().Dispose();
        }
    }
}
