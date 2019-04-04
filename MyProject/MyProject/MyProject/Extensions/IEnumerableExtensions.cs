using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Extensions
{
    public static class IEnumerableExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> source, IEnumerable<T> values)
        {
            foreach (var value in values)
                source.Add(value);
        }

        public static async Task<bool> AnyAsync<T>(this Task<IEnumerable<T>> source, Func<T, bool> predicate = null)
        {
            var result = await source;

            if (predicate != null)
                return result.Any(predicate);

            return result.Any();
        }

        public static async Task<List<T>> ToListAsync<T>(this Task<IEnumerable<T>> source)
        {
            return (await source).ToList();
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            ObservableCollection<T> collection = new ObservableCollection<T>();

            foreach (T item in source)
            {
                collection.Add(item);
            }

            return collection;
        }

        public static async Task<ObservableCollection<T>> ToObservableCollectionAsync<T>(this Task<IEnumerable<T>> source)
        {
            ObservableCollection<T> collection = new ObservableCollection<T>();

            foreach (T item in await source)
            {
                collection.Add(item);
            }

            return collection;
        }
    }
}
