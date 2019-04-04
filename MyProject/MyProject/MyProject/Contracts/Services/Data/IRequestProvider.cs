using System;
using System.Threading.Tasks;

namespace MyProject.Contracts.Services.Data
{
    public interface IRequestProvider
    {
        Task<TResult> GetAsync<TResult>(string uri, string token = "", TimeSpan? timeout = null);

        Task PostAsync(string uri, object data, string token = "", TimeSpan? timeout = null);

        Task<TResult> PostAsync<TResult>(string uri, object data, string token = "", TimeSpan? timeout = null);

        Task<TResult> PutAsync<TResult>(string uri, object data, string token = "", TimeSpan? timeout = null);

        Task DeleteAsync(string uri, string token = "", TimeSpan? timeout = null);
    }
}
