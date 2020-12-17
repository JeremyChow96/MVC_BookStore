﻿using System.Collections.Generic;
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
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager,
            IUserService userService,IEmailService emailService,IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _emailService = emailService;
            _configuration = configuration;
        }
        public async Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel)
        {
            var user = new ApplicationUser()
            {
                FirstName = userModel.FirstName,
                LastName =  userModel.LastName,
                Email =  userModel.Email,
                UserName = userModel.Email
            };
          
           var result =  await _userManager.CreateAsync(user, userModel.Password);
           if (result.Succeeded)
           {
               //Email confirm token
               var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
               if (!string.IsNullOrEmpty(token))
               {
                   await SendEmailConfirmationEmail(user, token);

               }
           }
           return result;
        }

        public async Task<SignInResult> PasswordSignInAsync(SignInModel signInModel)
        {
            var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password,
                signInModel.RememberMe, false);

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

        public async Task<IdentityResult> ConfirmEmaillAsync(string uid,string token)
        {
            return await _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync(uid),token);
        }

        private async Task SendEmailConfirmationEmail(ApplicationUser user,string token)
        {
            string appdomain = _configuration.GetSection("Application:Appdomain").Value;
            string confirmationLink = _configuration.GetSection("Application:EmailConfirmation").Value;


            UserEmailOptions options = new UserEmailOptions()
            {
                ToEmails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}",user.FirstName),
                    new KeyValuePair<string, string>("{{Link}}",string.Format(appdomain + confirmationLink,user.Id,token))

                }

            };

               await _emailService.SendEmailForConfirmation(options);

        }
    }
}