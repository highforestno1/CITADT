using Microsoft.AspNetCore.Mvc;

namespace CITADT.Controllers
{
    public class NewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
