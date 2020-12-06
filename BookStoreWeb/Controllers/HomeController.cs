using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Controllers
{
    public class HomeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }
    }
}