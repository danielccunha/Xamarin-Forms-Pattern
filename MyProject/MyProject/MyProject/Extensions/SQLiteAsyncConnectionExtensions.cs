using SQLite;
using System.Collections;
using System.Threading.Tasks;

namespace MyProject.Extensions
{
    public static class SQLiteAsyncConnectionExtensions
    {
        public static async Task DeleteManyAsync(this SQLiteAsyncConnection conn, IEnumerable entities)
        {
            foreach (var entity in entities)
                await conn.DeleteAsync(entity);
        }

        public static async Task TruncateAsync<TEntity>(this SQLiteAsyncConnection conn) where TEntity : new()
        {
            await conn.DropTableAsync<TEntity>();
            await conn.CreateTableAsync<TEntity>();
        }
    }
}
