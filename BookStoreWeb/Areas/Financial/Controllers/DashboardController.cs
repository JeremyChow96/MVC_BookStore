using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Areas.Financial.Controllers
{
    [Area("financial")]
    public class DashboardController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Graph()
        {
            return View();
        }
    }
}
