using MyProject.Contracts.Persistence;
using MyProject.Contracts.Persistence.Repositories;
using MyProject.UnitTests.Mocks.Persistence.Repositories;

namespace MyProject.UnitTests.Mocks.Persistence
{
    public class MockUnitOfWork : IUnitOfWork
    {
        public IProductRepository Products { get; } = new MockProductRepository();

        public void Dispose()
        {
            
        }
    }
}
