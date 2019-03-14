using System;

namespace MyProject.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        // TODO: Add repositories

        int Complete();
    }
}
