using MyProject.Contracts.Persistence.Repositories;
using System;
using System.Threading.Tasks;

namespace MyProject.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }

        Task InitializeAsync();
        Task TruncateTables();
    }
}
