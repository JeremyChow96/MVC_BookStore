using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BookStoreWeb.Repository;

namespace BookStoreWeb.Components
{
    public class TopBooksViewComponent:ViewComponent
    {
        private readonly BookRepository _bookRepository;

        public TopBooksViewComponent(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(int count)
        {
            var books = await _bookRepository.GetTopBooksAsync(count);
            return View(books);
        }


    }
}