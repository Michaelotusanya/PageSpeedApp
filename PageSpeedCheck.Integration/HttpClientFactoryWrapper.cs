using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PageSpeedCheck.Integration
{
    public class HttpClientFactoryWrapper : IHttpClientFactoryWrapper
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;

        public HttpClientFactoryWrapper(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _httpClient = _httpClientFactory.CreateClient();
        }

        public async Task<string> GetStringAsync(string url)
        {
            return  await _httpClient.GetStringAsync(url);
        }
    }
}
