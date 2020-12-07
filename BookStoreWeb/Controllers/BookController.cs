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
            var data = _bookRepository.GetAllBooks();
            return View(data);
        }
        [Route("book-detail/{id}",Name = "bookDetailRoute")]
        public IActionResult GetBook(int id)
        {
            var data = _bookRepository.GetBook(id);
            return View(data);
        }

        public IActionResult SearchBook(string title, string authorName)
        {
            
            return View();
        }
    }
}