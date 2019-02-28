using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using PageSpeedCheck.Integration.GoogleAPI;
using PageSpeedCheck.Integration.Tests.GoogleAPI.Data;
using PageSpeedCheck.Model.Dto;
using PageSpeedCheck.Model.Settings;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PageSpeedCheck.Integration.Tests.GoogleAPI
{
    [TestClass]
    public class PageSpeedTests
    {
        Mock<IMemoryCache> _memoryCacheMock;
        Mock<IHttpClientFactoryWrapper> _httpClientFactoryWrapperMock;
        Mock<IOptions<GoogleApiSettings>> _googleSettingsMock;
        private PageSpeedApi _pageSpeedApi;
        private string address = "http://www.test.com";

        [TestInitialize]
        public void Initialize()
        {
            _memoryCacheMock = new Mock<IMemoryCache>();
            _httpClientFactoryWrapperMock = new Mock<IHttpClientFactoryWrapper>();
            _googleSettingsMock = new Mock<IOptions<GoogleApiSettings>>();
        }

        [TestMethod]
        public async Task Can_Get_PageSpeed_Analytics_From_Google_Api()
        {
            // Arrange 
            var cache = new MemoryCache(new MemoryCacheOptions());
            var httpClientMock = new Mock<HttpClient>();
            _httpClientFactoryWrapperMock.Setup(x => x.GetStringAsync(It.IsAny<string>())).Returns(Task.FromResult(PageSpeedApiResponseData.Result()));
            _googleSettingsMock.Setup(x => x.Value)
                .Returns(new GoogleApiSettings { PageSpeedResourceUri = "/pagespeedonline/v1/runPagespeed?url={0}&key={1}", PageSpeedBaseAddress = address, ApiKey = "12312413" });
            _pageSpeedApi = new PageSpeedApi(cache, _httpClientFactoryWrapperMock.Object, _googleSettingsMock.Object);
            var testData = JsonConvert.DeserializeObject<PageSpeedDto>(PageSpeedApiResponseData.Result());

            // Act
            var result = await _pageSpeedApi.GetPageSpeedAnalyticsAsync(address);
            var data = JsonConvert.SerializeObject(result);

            // Assert
            Assert.AreEqual(data, JsonConvert.SerializeObject(testData));
        }

        [TestMethod]
        public async Task Can_Get_PageSpeed_Analytics_From_Cache()
        {
            // Arrange 
            var cache = new MemoryCache(new MemoryCacheOptions());
            var httpClientMock = new Mock<HttpClient>();
            cache.Set(address, JsonConvert.DeserializeObject<PageSpeedDto>(PageSpeedApiResponseData.Result()));
            _pageSpeedApi = new PageSpeedApi(cache, _httpClientFactoryWrapperMock.Object, _googleSettingsMock.Object);
            var testData = JsonConvert.DeserializeObject<PageSpeedDto>(PageSpeedApiResponseData.Result());

            // Act
            var result = await _pageSpeedApi.GetPageSpeedAnalyticsAsync(address);
            var data = JsonConvert.SerializeObject(result);
            // Assert
            Assert.AreEqual(data, JsonConvert.SerializeObject(testData));
        }

    }
}
