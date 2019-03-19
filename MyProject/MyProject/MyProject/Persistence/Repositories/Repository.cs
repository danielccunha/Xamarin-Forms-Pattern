using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MyProject.Contracts.Persistence.Repositories;
using SQLite;

namespace MyProject.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        private readonly SQLiteAsyncConnection _conn;

        public Repository(SQLiteAsyncConnection conn)
        {
            _conn = conn;
            _conn.CreateTableAsync<TEntity>().Wait();
        }

        public async Task<int> AddAsync(TEntity entity)
        {
            await _conn.InsertAsync(entity);

            var map = await _conn.GetMappingAsync<TEntity>();

            if (map.HasAutoIncPK)
                return (int)map.PK.GetValue(entity);

            return 0;
        }

        public Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            return _conn.InsertAllAsync(entities);
        }

        public Task<int> CountAsync()
        {
            return _conn.Table<TEntity>().CountAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _conn.Table<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _conn.Table<TEntity>().FirstOrDefaultAsync(predicate);
        }

        public Task<TEntity> GetAsync(object pk)
        {
            return _conn.FindAsync<TEntity>(pk);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _conn.Table<TEntity>().ToListAsync();
        }

        public async Task<bool> RemoveAsync(TEntity entity)
        {
            return await _conn.DeleteAsync(entity) > 0;
        }

        public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                await _conn.DeleteAsync(entity);
        }

        public async Task TruncateAsync()
        {
            await _conn.DropTableAsync<TEntity>();
            await _conn.CreateTableAsync<TEntity>();
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            return await _conn.UpdateAsync(entity) > 0;
        }

        public Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            return _conn.UpdateAllAsync(entities);
        }
    }
}
