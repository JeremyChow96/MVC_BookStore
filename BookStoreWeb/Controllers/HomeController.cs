﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Threading.Tasks;
using BookStoreWeb.Models;
using BookStoreWeb.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace BookStoreWeb.Controllers
{
   // [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IConfiguration _iConfiguration;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly NewBookAlertConfig newBookAlertConfig;
        private readonly NewBookAlertConfig thirdpartyBook;


        //IOptionsSnapshot works on scoped
        //IOptions works on singleton
        public HomeController(IConfiguration iConfiguration, IOptionsSnapshot<NewBookAlertConfig> iOptions,
            IMessageRepository messageRepository, IUserService userService,
            IEmailService emailService)
        {
            _iConfiguration = iConfiguration;
            _messageRepository = messageRepository;
            _userService = userService;
            _emailService = emailService;
            // newBookAlertConfig = iOptions.Value;
            newBookAlertConfig = iOptions.Get("InternalBook");
            thirdpartyBook = iOptions.Get("ThirdPartyBook");
        }


        [ViewData] public string CustomProperty { get; set; }

        // GET
      //  [Route("~/")]
        public async Task<IActionResult> Index()
        {

            //UserEmailOptions options =new UserEmailOptions()
            //{
            //    ToEmails = new List<string>(){"a283164069@outlook.com"},
            //    PlaceHolders = new List<KeyValuePair<string, string>>()
            //    {
            //        new KeyValuePair<string, string>("{{UserName}}","Jeremy"),
            //    }
            //};

            //await _emailService.SendTestEmail(options);


            //var g1 = Guid.NewGuid();
            //var g2 = Guid.NewGuid();
            //var g3 = Guid.NewGuid();


            var userId = _userService.GetUserId();
            var isLoggedIn = _userService.IsAuthenticated();

            var value = _messageRepository.Name();
            var newbookalert = new NewBookAlertConfig();
            _iConfiguration.Bind("NewBookAlert", newbookalert);


            var newbook = _iConfiguration.GetSection("NewBookAlert");
            var result = newbook.GetValue<bool>("DisplayNewBookAlert");
            var bookName = newbook.GetValue<string>("BookName");
            // icofiguration settings
            // var key3 = _iConfiguration.GetValue<bool>("DisplayNewBookAlert");
            // var sttt = _iConfiguration["DisplayNewBookAlert"];
            var key3 = _iConfiguration.GetValue<bool>("NewBookAlert:DisplayNewBookAlert");
            var key4 = _iConfiguration.GetValue<string>("NewBookAlert:BookName");

            // var result = _iConfiguration["AppName"];
            // var key1 = _iConfiguration["infoObj:key1"];
            // var key2 = _iConfiguration["infoObj:key2"];
            //
            // var key3 = _iConfiguration["infoObj:key3:key3obj1"];


            // ViewBag.Title = "  Jeremy!";
            //dynamic data = new ExpandoObject();
            //data.Id = 1;
            //data.Name = "Jeremy";
            //ViewBag.Data = data;
            ViewData["Name"] = "Jeremy";
            CustomProperty = "CustomeProperty Value";
            return View();
        }

        [Route("~/{controller}/about-us/{id?}")]
        public IActionResult AboutUs(int id)
        {
            return View();
        }

        [Authorize(Roles = "Admin,User")]  //确保只有登入账号才允许点击
        [Route("~/{controller}/contact-us")] //override route
        public IActionResult ContactUs()
        {



            return View();
        }
    }
}