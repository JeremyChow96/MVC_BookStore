using System.Threading.Tasks;
using BookStoreWeb.Models;
using Microsoft.AspNetCore.Identity;

namespace BookStoreWeb.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel);
        Task<SignInResult> PasswordSignInAsync(SignInModel signInModel);
        Task SignOutAsync();
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel model);
        Task<IdentityResult> ConfirmEmailAsync(string uid, string token);
        Task GenerateEmailConfirmationTonkenAsync(ApplicationUser user);

        Task<ApplicationUser> GetUserByEmailAsync(string email);
    }
}