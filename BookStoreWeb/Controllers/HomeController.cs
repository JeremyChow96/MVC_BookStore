using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace BookStoreWeb.Controllers
{
    public class HomeController : Controller
    {
        [ViewData]
        public string CustomProperty { get; set; }
        // GET
        public IActionResult Index()
        {
            // ViewBag.Title = "  Jeremy!";
            //dynamic data = new ExpandoObject();
            //data.Id = 1;
            //data.Name = "Jeremy";
            //ViewBag.Data = data;
             ViewData["Name"] = "Jeremy";
            CustomProperty = "CustomeProperty Value";
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }
    }
}