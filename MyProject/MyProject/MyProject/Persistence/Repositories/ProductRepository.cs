using MyProject.Contracts.Persistence.Domain;
using MyProject.Contracts.Persistence.Repositories;
using SQLite;

namespace MyProject.Persistence.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(SQLiteAsyncConnection conn) : base(conn)
        {

        }
    }
}
