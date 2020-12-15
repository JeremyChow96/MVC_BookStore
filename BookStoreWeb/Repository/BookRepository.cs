using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreWeb.Data;
using BookStoreWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BookStoreWeb.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context = null;
        private readonly IConfiguration _iConfiguration;

        public BookRepository(BookStoreContext context,IConfiguration iConfiguration)
        {
            _context = context;
            _iConfiguration = iConfiguration;
        }

        public async Task<int> AddNewBook(BookModel model)
        {
            var book = new Books()
            {
                Author = model.Author,
                CreateOn = DateTime.UtcNow,
                Description = model.Description,
                Title = model.Title,
                LanguageId = model.LanguageId,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
                UpdateOn = DateTime.UtcNow,
                CoverImageUrl = model.CoverImageUrl,
                BookPdfUrl = model.BookPdfUrl

            };

            book.bookGallery = new List<BookGallery>();
        //    var gallery = new List<BookGallery>();

            foreach (var file in model.Gallery)
            {
                book.bookGallery.Add(new BookGallery()
                {
                    Name = file.Name,
                    URL = file.URL,
                });
            }

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book.Id;
        }

        public async Task<List<BookModel>> GetAllBooks()
        {

            return await _context.Books.Select(book => new BookModel()
            {
                Author = book.Author,
                Title = book.Title,
                Description = book.Description,
                Category = book.Category,
                Id = book.Id,
                LanguageId = book.LanguageId,
                Language = book.Language.Name,
                TotalPages = book.TotalPages,
                CoverImageUrl = book.CoverImageUrl,
                BookPdfUrl =  book.BookPdfUrl
                
            }).ToListAsync();
            //return await _context.Books.Select(book);
        }


        public async Task<List<BookModel>> GetTopBooksAsync(int count)
        {

            return await _context.Books.Select(book => new BookModel()
            {
                Author = book.Author,
                Title = book.Title,
                Description = book.Description,
                Category = book.Category,
                Id = book.Id,
                LanguageId = book.LanguageId,
                Language = book.Language.Name,
                TotalPages = book.TotalPages,
                CoverImageUrl = book.CoverImageUrl,
            }).Take(count).ToListAsync();
            //return await _context.Books.Select(book);
        }

        public async Task<BookModel> GetBookById(int id)
        {
            //利用Select语句 使用外键
            var book = await _context.Books.Where(x => x.Id == id)
                .Select(book => new BookModel()
                {
                    Author = book.Author,
                    Title = book.Title,
                    Description = book.Description,
                    Category = book.Category,
                    Id = book.Id,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    TotalPages = book.TotalPages,
                    CoverImageUrl = book.CoverImageUrl,
                    BookPdfUrl = book.BookPdfUrl,
                    Gallery = book.bookGallery.Select(g => new GalleryModel()
                    {
                        Id = g.Id,
                        Name = g.Name,
                        URL = g.URL,
                    }).ToList()
                }).FirstOrDefaultAsync();
           

            return book;
            // return   await _context.Books.Where(c=>c.Id==id).FirstOrDefaultAsync();
        }

        public List<BookModel> SearchBook(string titile, string authorName)
        {
            return DataSouce().Where(c => c.Title.Contains(titile) || c.Author.Contains(authorName)).ToList();
        }

        public List<BookModel> DataSouce()
        {
            //return new List<BookModel>()
            //{
            //    new BookModel(){Id=1, Title = "1", Author = "1", Category = "1",Description = "1111",LanguageId = "Zh",TotalPages = 100},
            //    new BookModel(){Id=2, Title = "2", Author = "2", Category = "2",Description = "2222",LanguageId = "Zh",TotalPages = 100},
            //    new BookModel(){Id=3, Title = "3", Author = "3", Category = "3",Description = "3333",LanguageId = "Zh",TotalPages = 100},
            //    new BookModel(){Id=4, Title = "4", Author = "4", Category = "4",Description = "4444",LanguageId = "Zh",TotalPages = 100},
            //    new BookModel(){Id=5, Title = "5", Author = "5", Category = "5",Description = "5555",LanguageId = "Zh",TotalPages = 100},

            //};
            return null;
        }
    }
}