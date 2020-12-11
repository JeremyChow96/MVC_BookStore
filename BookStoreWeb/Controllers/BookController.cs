using System.Threading.Tasks;
using BookStoreWeb.Models;
using BookStoreWeb.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStoreWeb.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository = null;
        private readonly LanguageRepository _languageRepository = null;

        public BookController(BookRepository bookRepository, LanguageRepository languageRepository)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
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

        public async Task<IActionResult> AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            var model = new BookModel()
            {

            };

            ViewBag.Language = new SelectList(await _languageRepository.GetLanguages(),"Id", "Description");  


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
            // ModelState.AddModelError("","This is my custom error message");
            ViewBag.Language = new SelectList(await _languageRepository.GetLanguages(), "Id", "Description");



            return View();
        }
    }
}