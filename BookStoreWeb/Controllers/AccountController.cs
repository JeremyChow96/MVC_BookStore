using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStoreWeb.Models;
using BookStoreWeb.Repository;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;


        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [Route("signup")]
        public IActionResult SignUp()
        {
            return View();
        }

        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpUserModel userModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.CreateUserAsync(userModel);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(userModel);
                }

                ModelState.Clear();

                //注册成功后  进行邮箱验证
                return RedirectToAction("ConfirmEmail", new {email = userModel.Email});
            }

            return View();
        }


        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(SignInModel signIn, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.PasswordSignInAsync(signIn);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }

                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError("", "Not allowed to login");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid credentials");
                }
            }

            return View();
        }

        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountRepository.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [Route("change-password")]
        public IActionResult ChangePasswod()
        {
            return View();
        }


        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePasswod(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.ChangePasswordAsync(model);
                if (result.Succeeded)
                {
                    ViewBag.IsSuccess = true;
                    ModelState.Clear();
                    return View();
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        //  [Route("confirm-email?uid={0}%token={1}")]
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string uid, string token,string email)
        {
            EmailConfirmModel model = new EmailConfirmModel()
            {
                Email = email
            };

            if (!string.IsNullOrEmpty(uid) && !string.IsNullOrEmpty(token))
            {
                token = token.Replace(' ', '+');
                var result = await _accountRepository.ConfirmEmailAsync(uid, token);
                if (result.Succeeded)
                {
                   // ViewBag.IsSuccess = true;
                   model.EmailVerify = true;
                }
            }

            return View(model);
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(EmailConfirmModel model)
        {
            var user = await _accountRepository.GetUserByEmailAsync(model.Email);
            if (user != null)
            {
                if (user.EmailConfirmed)
                {
                    model.EmailVerify = true;
                    return View(model);
                }

                await _accountRepository.GenerateEmailConfirmationTonkenAsync(user);
                model.EmailSent = true;

                ModelState.Clear();
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong.");
            }

            return View(model);
        }



        [HttpPost("forget-password"),AllowAnonymous]
        public async Task<IActionResult> ForgetPassword()
        {


            return View();
        }

        [HttpPost("forget-password"), AllowAnonymous]
        public async Task<IActionResult> ForgetPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {


                ModelState.Clear();
                model.EmailSent = true;
            }

            return View(model);
        }
    
}
}