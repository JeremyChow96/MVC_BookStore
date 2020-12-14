using System.Collections.Generic;
using System.Threading.Tasks;
using BookStoreWeb.Models;

namespace BookStoreWeb.Repository
{
    public interface IBookRepository
    {
        Task<int> AddNewBook(BookModel model);
        Task<List<BookModel>> GetAllBooks();
        Task<List<BookModel>> GetTopBooksAsync(int count);
        Task<BookModel> GetBookById(int id);
        List<BookModel> SearchBook(string titile, string authorName);
        List<BookModel> DataSouce();
    }
}