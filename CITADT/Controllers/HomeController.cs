using System.Diagnostics;
using CITADT.Models;
using Microsoft.AspNetCore.Mvc;

namespace CITADT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult News1()
        {
            return View();
        }

        public IActionResult News()
        {
            return View();
        }

        public IActionResult Student()
        {
            return View();
        }

        public IActionResult MiningGuide()
        {
            return View();
        }

        public IActionResult FormProcess()
        {
            return View();
        }

        public IActionResult Training()
        {
            return View();
        }

        public IActionResult Organization()
        {
            return View();
        }
        public IActionResult Schedule()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }

        public IActionResult Certificate()
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
