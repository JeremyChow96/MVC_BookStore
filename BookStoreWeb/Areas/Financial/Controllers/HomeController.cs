using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoreWeb.Areas.Financial.Controllers
{
    [Area("financial")]
    public class HomeController : Controller
    {
    
        public IActionResult Index()
        {
            return View();
        }
    }
}
