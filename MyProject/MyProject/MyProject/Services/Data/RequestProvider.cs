using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MyProject.Contracts.Services.Data;
using MyProject.Exceptions;
using Newtonsoft.Json;

namespace MyProject.Services.Data
{
    public class RequestProvider : IRequestProvider
    {
        private readonly int _timeoutSeconds = 30;
        private readonly Encoding _encoding = Encoding.UTF8;
        private readonly string _mediaType = "application/json";

        public async Task DeleteAsync(string uri, string token = "", TimeSpan? timeout = null)
        {
            using (var httpClient = CreateHttpClient(token, timeout))
            {
                await httpClient.DeleteAsync(uri);
            }
        }

        public async Task<TResult> GetAsync<TResult>(string uri, string token = "", TimeSpan? timeout = null)
        {
            using (var httpClient = CreateHttpClient(token, timeout))
            {
                var response = await httpClient.GetAsync(uri);

                await HandleResponse(response);

                string serialized = await response.Content.ReadAsStringAsync();
                TResult result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(serialized));

                return result;
            }
        }

        public Task<TResult> PostAsync<TResult>(string uri, TResult data, string token = "", string header = "", TimeSpan? timeout = null)
        {
            throw new NotImplementedException();
        }

        public Task<TResult> PostAsync<TResult>(string uri, string data, string clientId, string clientSecret, TimeSpan? timeout = null)
        {
            throw new NotImplementedException();
        }

        public async Task<TResult> PutAsync<TResult>(string uri, TResult data, string token = "", TimeSpan? timeout = null)
        {
            using (var httpClient = CreateHttpClient(token, timeout))
            {
                var content = new StringContent(JsonConvert.SerializeObject(data), _encoding);
                content.Headers.ContentType = new MediaTypeHeaderValue(_mediaType);

                var response = await httpClient.PutAsync(uri, content);

                await HandleResponse(response);

                string serialized = await response.Content.ReadAsStringAsync();
                TResult result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(serialized));

                return result;
            }
        }

        private HttpClient CreateHttpClient(string token, TimeSpan? timeout)
        {
            var httpClient = new HttpClient { Timeout = timeout ?? TimeSpan.FromSeconds(_timeoutSeconds) };
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_mediaType));

            if (!string.IsNullOrEmpty(token))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return httpClient;
        }

        private async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
                    throw new ServiceAuthenticationException(content);

                throw new RequestException(response.StatusCode, content);
            }
        }
    }
}
