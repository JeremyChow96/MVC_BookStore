using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreWeb.Data;
using BookStoreWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreWeb.Repository
{
    public class BookRepository
    {
        private readonly BookStoreContext _context = null;

        public BookRepository(BookStoreContext context)
        {
            _context = context;
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
                UpdateOn = DateTime.UtcNow
            };

            await _context.Books.AddAsync(book);
            ;
            await _context.SaveChangesAsync();
            return book.Id;
        }

        public async Task<List<BookModel>> GetAllBooks()
        {
            var books = new List<BookModel>();
            var allbooks = await _context.Books.ToListAsync();
            if (allbooks?.Any() == true)
            {
                foreach (var book in allbooks)
                {
                    books.Add(new BookModel()
                    {
                        Author = book.Author,
                        Title = book.Title,
                        Description = book.Description,
                        Category = book.Category,
                        Id = book.Id,
                        LanguageId = book.LanguageId,
                        TotalPages = book.TotalPages
                    });
                }
            }

            return books;
        }

        public async Task<BookModel> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                var bookDetails = new BookModel()
                {
                    Author = book.Author,
                    Title = book.Title,
                    Description = book.Description,
                    Category = book.Category,
                    Id = book.Id,
                    LanguageId = book.LanguageId,
                    TotalPages = book.TotalPages
                };
                return bookDetails;
            }

            return null;
            // return   await _context.Books.Where(c=>c.Id==id).FirstOrDefaultAsync();
        }

        public List<BookModel> SearchBook(string titile, string authorName)
        {
            return DataSouce().Where(c => c.Title.Contains(titile) || c.Author.Contains(authorName)).ToList();
        }

        private List<BookModel> DataSouce()
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