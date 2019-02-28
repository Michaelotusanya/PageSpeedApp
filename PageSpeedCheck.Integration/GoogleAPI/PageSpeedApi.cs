using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PageSpeedCheck.Model.Dto;
using PageSpeedCheck.Model.Settings;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PageSpeedCheck.Integration.GoogleAPI
{
    public class PageSpeedApi : IPageSpeedApi
    {
        private IHttpClientFactoryWrapper _httpClientFactoryWrapper;
        private IOptions<GoogleApiSettings> _googleSettings;
        private readonly IMemoryCache _memoryCache;

        public PageSpeedApi(IMemoryCache memoryCache, IHttpClientFactoryWrapper httpClientFactoryWrapper, IOptions<GoogleApiSettings> googleSettings)
        {
            _httpClientFactoryWrapper = httpClientFactoryWrapper;
            _googleSettings = googleSettings;
            _memoryCache = memoryCache;
        }

        public async Task<PageSpeedDto> GetPageSpeedAnalyticsAsync(string url)
        {
            var pageSpeedAnalytics = _memoryCache.Get(url ?? string.Empty) as PageSpeedDto;
            if (pageSpeedAnalytics == null)
            {
                var resourceUri = string.Format(_googleSettings.Value.PageSpeedResourceUri, url, _googleSettings.Value.ApiKey);
                var response = await _httpClientFactoryWrapper.GetStringAsync(_googleSettings.Value.PageSpeedBaseAddress + resourceUri);

                pageSpeedAnalytics = JsonConvert.DeserializeObject<PageSpeedDto>(response);
                _memoryCache.Set(url, pageSpeedAnalytics, TimeSpan.FromMinutes(30));
            }

            return pageSpeedAnalytics;
        }

    }
}
