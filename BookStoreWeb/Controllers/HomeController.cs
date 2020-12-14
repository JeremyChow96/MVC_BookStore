using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace BookStoreWeb.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        [ViewData]
        public string CustomProperty { get; set; }
        // GET
        [Route("~/")]
     
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

        [Route("~/{controller}/about-us/{id}")]
     
        public IActionResult AboutUs(int id)
        {
            return View();
        }

        [Route("~/{controller}/contact-us")]  //override route
        public IActionResult ContactUs()
        {
            return View();
        }
    }
}