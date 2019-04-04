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
                await httpClient.DeleteAsync(uri).ConfigureAwait(continueOnCapturedContext: false);
            }
        }

        public async Task<TResult> GetAsync<TResult>(string uri, string token = "", TimeSpan? timeout = null)
        {
            using (var httpClient = CreateHttpClient(token, timeout))
            {
                var response = await httpClient.GetAsync(uri).ConfigureAwait(continueOnCapturedContext: false);

                await HandleResponse(response).ConfigureAwait(continueOnCapturedContext: false);

                string serialized = await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);
                TResult result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(serialized)).ConfigureAwait(continueOnCapturedContext: false);

                return result;
            }
        }

        public async Task PostAsync(string uri, object data, string token = "", TimeSpan? timeout = null)
        {
            using (var httpClient = CreateHttpClient(token, timeout))
            {
                var content = new StringContent(JsonConvert.SerializeObject(data), _encoding);
                content.Headers.ContentType = new MediaTypeHeaderValue(_mediaType);

                var response = await httpClient.PostAsync(uri, content).ConfigureAwait(continueOnCapturedContext: false);

                await HandleResponse(response).ConfigureAwait(continueOnCapturedContext: false);
            }
        }

        public async Task<TResult> PostAsync<TResult>(string uri, object data, string token = "", TimeSpan? timeout = null)
        {
            using (var httpClient = CreateHttpClient(token, timeout))
            {
                var content = new StringContent(JsonConvert.SerializeObject(data), _encoding);
                content.Headers.ContentType = new MediaTypeHeaderValue(_mediaType);

                var response = await httpClient.PostAsync(uri, content).ConfigureAwait(continueOnCapturedContext: false);

                await HandleResponse(response).ConfigureAwait(continueOnCapturedContext: false);

                string serialized = await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);
                TResult result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(serialized)).ConfigureAwait(continueOnCapturedContext: false);

                return result;
            }
        }

        public async Task<TResult> PutAsync<TResult>(string uri, object data, string token = "", TimeSpan? timeout = null)
        {
            using (var httpClient = CreateHttpClient(token, timeout))
            {
                var content = new StringContent(JsonConvert.SerializeObject(data), _encoding);
                content.Headers.ContentType = new MediaTypeHeaderValue(_mediaType);

                var response = await httpClient.PutAsync(uri, content).ConfigureAwait(continueOnCapturedContext: false);

                await HandleResponse(response).ConfigureAwait(continueOnCapturedContext: false);

                string serialized = await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);
                TResult result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(serialized)).ConfigureAwait(continueOnCapturedContext: false);

                return result;
            }
        }

        private HttpClient CreateHttpClient(string token, TimeSpan? timeout)
        {
            var handler = new HttpClientHandler { CookieContainer = new CookieContainer() };
            var httpClient = new HttpClient(handler) { Timeout = timeout ?? TimeSpan.FromSeconds(_timeoutSeconds) };
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
