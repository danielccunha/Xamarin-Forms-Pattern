using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MyProject.Contracts.Persistence.Domain;
using MyProject.Contracts.Persistence.Repositories;

namespace MyProject.UnitTests.Mocks.Persistence.Repositories
{
    public class MockProductRepository : IProductRepository
    {
        private List<Product> Products { get; } = new List<Product>
        {
            new Product { Id = 1, Name = "Product A", Price = 10 },
            new Product { Id = 2, Name = "Product B", Price = 20 },
            new Product { Id = 3, Name = "Product C", Price = 30 },
            new Product { Id = 4, Name = "Product D", Price = 40 },
            new Product { Id = 5, Name = "Product E", Price = 50 },
        };

        public Task<int> AddAsync(Product entity)
        {
            entity.Id = Products.Count + 1;
            Products.Add(entity);

            return Task.FromResult(entity.Id);
        }

        public Task AddRangeAsync(IEnumerable<Product> entities)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> FindAsync(Expression<Func<Product, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Product> FirstOrDefaultAsync(Expression<Func<Product, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync() => Task.FromResult<IEnumerable<Product>>(Products);

        public Task<Product> GetAsync(object pk)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(Product entity)
        {
            try
            {
                Products.Remove(entity);
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }

        public Task RemoveRangeAsync(IEnumerable<Product> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRangeAsync(IEnumerable<Product> entities)
        {
            throw new NotImplementedException();
        }
    }
}
