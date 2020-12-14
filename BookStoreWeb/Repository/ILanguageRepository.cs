using System.Collections.Generic;
using System.Threading.Tasks;
using BookStoreWeb.Models;

namespace BookStoreWeb.Repository
{
    public interface ILanguageRepository
    {
        Task<List<LanguageModel>> GetLanguages();
    }
}