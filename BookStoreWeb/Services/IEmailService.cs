using System.Threading.Tasks;
using BookStoreWeb.Models;

namespace BookStoreWeb.Services
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions userEmailOptions);
        Task SendEmailForConfirmation(UserEmailOptions userEmailOptions);
        Task SendEmailForForgoPassword(UserEmailOptions userEmailOptions);
    }
}