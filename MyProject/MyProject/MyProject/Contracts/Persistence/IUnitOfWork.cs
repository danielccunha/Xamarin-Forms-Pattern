using MyProject.Contracts.Persistence.Repositories;
using System;

namespace MyProject.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
    }
}
