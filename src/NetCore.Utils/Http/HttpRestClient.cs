using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NetCore.Utils.Http
{
    public interface IHttpRestClient
    {
        Task<T> GetAsync<T>(string url, HttpClientOption option = null);

        Task<T> PostAsync<T>(string url, object query = null, HttpClientOption option = null);

        Task<bool> PostAsync(string url, object query = null, HttpClientOption option = null);

        Task<T> PutAsync<T>(string url, object query = null, HttpClientOption option = null);

        Task<bool> PutAsync(string url, object query = null, HttpClientOption option = null);

        Task<T> DeleteAsync<T>(string url, HttpClientOption option = null);

        Task<bool> DeleteAsync(string url, HttpClientOption option = null);
    }

    public class HttpRestClient : IHttpRestClient
    {
        public async Task<T> GetAsync<T>(string url, HttpClientOption option = null)
        {
            using (var client = GetClient(option))
            {
                using (var response = await client.GetAsync(url))
                {
                    response.EnsureSuccessStatusCode();
                    var stream = await response.Content.ReadAsStreamAsync();
                    return Deserialize<T>(stream);
                }
            }
        }

        public async Task<T> PostAsync<T>(string url, object query = null, HttpClientOption option = null)
        {
            using (var client = GetClient(option))
            {
                using (var response = await client.PostAsync(url, Serialize(query)))
                {
                    response.EnsureSuccessStatusCode();
                    var stream = await response.Content.ReadAsStreamAsync();
                    return Deserialize<T>(stream);
                }
            }
        }

        public async Task<bool> PostAsync(string url, object query = null, HttpClientOption option = null)
        {
            using (var client = GetClient(option))
            {
                using (var response = await client.PostAsync(url, Serialize(query)))
                {
                    return response.IsSuccessStatusCode;
                }
            }
        }

        public async Task<T> PutAsync<T>(string url, object query = null, HttpClientOption option = null)
        {
            using (var client = GetClient(option))
            {
                using (var response = await client.PutAsync(url, Serialize(query)))
                {
                    response.EnsureSuccessStatusCode();
                    var stream = await response.Content.ReadAsStreamAsync();
                    return Deserialize<T>(stream);
                }
            }
        }

        public async Task<bool> PutAsync(string url, object query = null, HttpClientOption option = null)
        {
            using (var client = GetClient(option))
            {
                using (var response = await client.PutAsync(url, Serialize(query)))
                {
                    return response.IsSuccessStatusCode;
                }
            }
        }

        public async Task<T> DeleteAsync<T>(string url, HttpClientOption option = null)
        {
            using (var client = GetClient(option))
            {
                using (var response = await client.DeleteAsync(url))
                {
                    response.EnsureSuccessStatusCode();
                    var stream = await response.Content.ReadAsStreamAsync();
                    return Deserialize<T>(stream);
                }
            }
        }

        public async Task<bool> DeleteAsync(string url, HttpClientOption option = null)
        {
            using (var client = GetClient(option))
            {
                using (var response = await client.DeleteAsync(url))
                {
                    return response.IsSuccessStatusCode;
                }
            }
        }

        private static HttpClient GetClient(HttpClientOption option = null)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (option != null)
            {
                if (option.Timeout.HasValue)
                {
                    client.Timeout = option.Timeout.Value;
                }

                if (option.MaxResponseContentBufferSize.HasValue)
                {
                    client.MaxResponseContentBufferSize = option.MaxResponseContentBufferSize.Value;
                }

                if (option.Headers != null)
                {
                    foreach (var header in option.Headers)
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                }
            }
            return client;
        }

        private static HttpContent Serialize(object data)
        {
            if (data == null)
            {
                return null;
            }

            var json = JsonConvert.SerializeObject(data);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private static T Deserialize<T>(Stream stream)
        {
            using (var reader = new JsonTextReader(new StreamReader(stream)))
            {
                var serializer = new JsonSerializer();
                return (T)serializer.Deserialize(reader, typeof(T));
            }
        }
    }

    public class HttpClientOption
    {
        public Dictionary<string, string> Headers { get; set; }

        public TimeSpan? Timeout { get; set; }

        public long? MaxResponseContentBufferSize { get; set; }
    }
}