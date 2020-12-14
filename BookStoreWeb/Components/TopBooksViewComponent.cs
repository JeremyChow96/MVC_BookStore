using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BookStoreWeb.Repository;

namespace BookStoreWeb.Components
{
    public class TopBooksViewComponent:ViewComponent
    {
        private readonly IBookRepository _bookRepository;

        public TopBooksViewComponent(IBookRepository bookRepository)
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