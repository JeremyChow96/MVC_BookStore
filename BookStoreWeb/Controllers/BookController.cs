using System.Threading.Tasks;
using BookStoreWeb.Models;
using BookStoreWeb.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository = null;

        public BookController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // GET
        public async Task<IActionResult> GetAllBooks()
        {
            var data = await _bookRepository.GetAllBooks();
            return View(data);
        }

        [Route("book-detail/{id}", Name = "bookDetailRoute")]
        public async Task<IActionResult> GetBook(int id)
        {
            var data = await _bookRepository.GetBookById(id);
            return View(data);
        }

        public IActionResult SearchBook(string title, string authorName)
        {
            return View();
        }

        public IActionResult AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                var id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new {isSuccess = true, bookId = id});
                }
            }
            ModelState.AddModelError("","This is my custom error message");
     
            return View();
        }
    }
}