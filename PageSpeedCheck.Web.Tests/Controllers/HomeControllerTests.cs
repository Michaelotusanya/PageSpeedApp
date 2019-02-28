using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PageSpeedCheck.Integration.GoogleAPI;
using PageSpeedCheck.Model.Dto;
using PageSpeedCheck.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PageSpeedCheck.Web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        private Mock<IPageSpeedApi> _pageSpeedApiMock;
        private HomeController _homeController;
        private string address = "http://www.test.com";

        [TestInitialize]
        public void Setup()
        {
            _pageSpeedApiMock = new Mock<IPageSpeedApi>();
            _pageSpeedApiMock.Setup(x => x.GetPageSpeedAnalyticsAsync(address))
                .Returns(Task.FromResult(new Model.Dto.PageSpeedDto { id = address }));

            _homeController = new HomeController(_pageSpeedApiMock.Object);
        }

        [TestMethod]
        public async Task Can_Get_PageSpeed_Analytics()
        {
            // Act
            var result = await _homeController.GetPageSpeedAnalytics(new Models.SearchForm { Url = address }) as ViewResult;

            // Assert 
            Assert.IsNotNull(result);
            var model = result.Model as PageSpeedDto;
            Assert.IsNotNull(model);
            Assert.IsTrue(model.id == address);
        }
    }
}
