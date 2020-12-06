using BookStoreWeb.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository = null;

        public BookController()
        {
            _bookRepository =new BookRepository();
        }
        // GET
        public IActionResult GetAllBooks()
        {
            return View();
        }

        public IActionResult GetBook(int id)
        {
            return View();
        }

        public IActionResult SearchBook(string title, string authorName)
        {
            
            return View();
        }
    }
}