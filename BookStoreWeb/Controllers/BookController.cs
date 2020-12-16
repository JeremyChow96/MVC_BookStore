using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;
using BookStoreWeb.Models;
using BookStoreWeb.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStoreWeb.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILanguageRepository _languageRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public BookController(IBookRepository bookRepository, 
            ILanguageRepository languageRepository,
            IWebHostEnvironment webHostEnvironment)
        {
       
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET
        public async Task<IActionResult> GetAllBooks()
        {
            var data = await _bookRepository.GetAllBooks();
            return View(data);
        }

        [Route("book-detail/{id:int:min(1)}", Name = "bookDetailRoute")]
        public async Task<IActionResult> GetBook(int id)
        {
            var data = await _bookRepository.GetBookById(id);
            return View(data);
        }

        public IActionResult SearchBook(string title, string authorName)
        {
            return View();
        }
        [Authorize]
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
                if (bookModel.CoverPhoto!=null)
                {
                    string folder = "books/cover/";
                 bookModel.CoverImageUrl   = await UploadFile(folder,bookModel.CoverPhoto);
                }


                if (bookModel.GalleryFiles != null)
                {
                    bookModel.Gallery = new List<GalleryModel>();
                    string folder = "books/gallery/";
                    foreach (var file in bookModel.GalleryFiles)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.Name,
                            URL = await UploadFile(folder, file)
                        };
                        bookModel.Gallery.Add(gallery);
                    }
                }


                if (bookModel.BookPdf != null)
                {
                    string folder = "books/pdf/";
                    bookModel.BookPdfUrl = await UploadFile(folder, bookModel.BookPdf);
                }

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

        private async Task<string> UploadFile(string folderPath,IFormFile file)
        {

            folderPath += Guid.NewGuid() + "_" + file.FileName;
      
            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            //if (!Directory.Exists(Path.GetDirectoryName(serverFolder)))
            //{
            //    Directory.CreateDirectory(Path.GetDirectoryName(serverFolder));
            //}

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }

        //route attribute :1 .alpha:minlength(5):regex()
        //
    }
}