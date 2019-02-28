using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PageSpeedCheck.Integration.GoogleAPI;
using PageSpeedCheck.Model.Dto;
using PageSpeedCheck.Web.Models;

namespace PageSpeedCheck.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPageSpeedApi _pageSpeedApi;
        
        public HomeController(IPageSpeedApi pageSpeedApi)
        {
            _pageSpeedApi = pageSpeedApi;
        }

        public IActionResult Index()
        {
            return View(new PageSpeedDto());
        }

        [HttpGet]
        public async Task<IActionResult> GetPageSpeedAnalytics(SearchForm form)
        {
            var pageSpeedAnalytics = await _pageSpeedApi.GetPageSpeedAnalyticsAsync(form.Url);
            return View("Index", pageSpeedAnalytics);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
