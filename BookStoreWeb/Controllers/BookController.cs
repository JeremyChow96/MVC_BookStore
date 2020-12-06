using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Controllers
{
    public class BookController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}