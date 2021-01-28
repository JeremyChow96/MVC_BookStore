using System.Collections.Generic;
using System.Threading.Tasks;
using BookStoreWeb.Models;
using BookStoreWeb.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace BookStoreWeb.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IUserService userService, IEmailService emailService, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _emailService = emailService;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            //_userManager.AddToRoleAsync()
            return await _userManager.FindByEmailAsync(email);
        }


        public async Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel)
        {
            var user = new ApplicationUser()
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                UserName = userModel.Email
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);
            if (result.Succeeded)
            {
                await GenerateEmailConfirmationTonkenAsync(user);
                //Email confirm token
                //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //if (!string.IsNullOrEmpty(token))
                //{
                //    await SendEmailConfirmationEmail(user, token);

                //}
            }

            return result;
        }


        public async Task GenerateEmailConfirmationTonkenAsync(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendEmailConfirmationEmail(user, token);
            }
        }

        public async Task GenerateForgotPasswordTonkenAsync(ApplicationUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendForgotpasswordEmail(user, token);
            }
        }

        public async Task<SignInResult> PasswordSignInAsync(SignInModel signInModel)
        {
            var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password,
                signInModel.RememberMe, true);
            //默认情况下 5次错误 账号被锁

            return result;
        }


        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }


        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel model)
        {
            var userId = _userService.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string uid, string token)
        {
            return await _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync(uid), token);
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordModel model)
        {
            return await _userManager.ResetPasswordAsync(await _userManager.FindByIdAsync(model.UserId), model.Token,
                model.NewPassword);
        }

        private async Task SendEmailConfirmationEmail(ApplicationUser user, string token)
        {
            string appdomain = _configuration.GetSection("Application:Appdomain").Value;
            string confirmationLink = _configuration.GetSection("Application:EmailConfirmation").Value;


            UserEmailOptions options = new UserEmailOptions()
            {
                ToEmails = new List<string>() {user.Email},
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.FirstName),
                    //Link  地址 + 路由
                    new KeyValuePair<string, string>("{{Link}}",
                        string.Format(appdomain + confirmationLink, user.Id, token))
                }
            };

            await _emailService.SendEmailForConfirmation(options);
        }

        private async Task SendForgotpasswordEmail(ApplicationUser user, string token)
        {
            string appdomain = _configuration.GetSection("Application:Appdomain").Value;
            string confirmationLink = _configuration.GetSection("Application:ForgotPassword").Value;


            UserEmailOptions options = new UserEmailOptions()
            {
                ToEmails = new List<string>() {user.Email},
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.FirstName),
                    //Link  地址 + 路由
                    new KeyValuePair<string, string>("{{Link}}",
                        string.Format(appdomain + confirmationLink, user.Id, token))
                }
            };

            await _emailService.SendEmailForForgoPassword(options);
        }
    }
}