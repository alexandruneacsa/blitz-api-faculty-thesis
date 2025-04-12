using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Blitz.Application.Helpers
{
    public static class ApiHelper
    {
        private static readonly HttpClient _httpClient;

        static ApiHelper()
        {
            _httpClient = new HttpClient();
        }

        public static async Task<T> GetAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var @object = JsonConvert.DeserializeObject<T>(content);
                return @object;
            }
            else
            {
                throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}");
            }
        }

        public static async Task<TResponse> PostAsync<TRequest, TResponse>(string url, TRequest requestData)
        {
            var response = await _httpClient.PostAsJsonAsync(url, requestData);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var @object = JsonConvert.DeserializeObject<TResponse>(content);
                return @object;
            }
            else
            {
                throw new HttpRequestException($"HTTP request failed with status code {response.StatusCode}");
            }
        }

        public static async Task<byte[]> GetByteFromUrl(string url)
        {
            try
            {
                byte[] content;
                using (HttpClient client = new())
                {
                    content = await client.GetByteArrayAsync(url);
                }

                return content;
            }
            catch (Exception)
            {
                return new byte[] { };
            }
        }
    }
}