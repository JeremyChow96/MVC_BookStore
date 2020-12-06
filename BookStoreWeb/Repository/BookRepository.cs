using System.Collections.Generic;
using System.Linq;
using BookStoreWeb.Models;

namespace BookStoreWeb.Repository
{
    public class BookRepository
    {
        public List<BookModel> GetAllBooks()
        {
            return DataSouce();
        }
        public BookModel GetBook(int id)
        {
            return DataSouce().Find(c => c.Id == id);
        }

        public List<BookModel> SearchBook(string titile,string authorName)
        {
            return DataSouce().Where(c => c.Title.Contains(titile) || c.Author.Contains(authorName)).ToList();
        }

        private List<BookModel> DataSouce()
        {
            return new List<BookModel>()
            {
                new BookModel(){Id=1, Title = "1", Author = "1", Category = "1"},
                new BookModel(){Id=2, Title = "2", Author = "2", Category = "2"},
                new BookModel(){Id=3, Title = "3", Author = "3", Category = "3"},
                new BookModel(){Id=4, Title = "4", Author = "4", Category = "4"},
                new BookModel(){Id=5, Title = "5", Author = "5", Category = "5"},


            };
    }
    }
}