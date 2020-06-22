using System.Collections.Generic;
using System.Diagnostics;
using ClassLibrary_Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplicationMVC_Diploma.Models;

namespace WebApplicationMVC_Diploma.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IClient _client;
        private readonly IRequestResponseModel _requestResponse;

        public HomeController(ILogger<HomeController> logger, IClient client, IRequestResponseModel requestResponse)
        {
            _logger = logger;
            _client = client;
            _requestResponse = requestResponse;
        }

        public IActionResult Index(string SearchTerm = null)
        {
            if (SearchTerm != null)
            {
                _requestResponse.SearchTerm = SearchTerm;
                _requestResponse.ResponseList = new List<string>();
                foreach (var str in _client.GetHumanValue(SearchTerm))
                    _requestResponse.ResponseList.Add(str);
            }
            return View(_requestResponse);
        }

        public IActionResult Download()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
