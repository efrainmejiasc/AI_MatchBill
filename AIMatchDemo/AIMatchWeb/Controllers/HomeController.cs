using Microsoft.AspNetCore.Mvc;

namespace AIMatchWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
