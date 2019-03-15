using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MyProject.Extensions
{
    public static class ObservableExtensions
    {
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
