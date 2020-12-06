using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Controllers
{
    public class HomeController : Controller
    {
        // GET
        public string Index()
        {
            return "Hell";
        }
    }
}