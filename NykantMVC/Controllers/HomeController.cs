﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NykantMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using NykantMVC.Extensions;
using System;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using NykantMVC.Friends;
using System.Text.Encodings.Web;
using NykantMVC.Models.ViewModels;
using NykantMVC.Services;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace NykantMVC.Controllers
{
    [AutoValidateAntiforgeryToken]
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        private readonly IProtectionService _protectionService;
        private readonly IMailService mailService;
        private  IHostEnvironment Environment { get; set; }
        public HomeController(ILogger<HomeController> logger, IHostEnvironment _environment, IMailService _mailService, IProtectionService protectionService, IOptions<Urls> urls, HtmlEncoder htmlEncoder) : base(logger, urls, htmlEncoder)
        {
            _protectionService = protectionService;
            Environment = _environment;
            mailService = _mailService;
        }

        public async Task<IActionResult> Index()
        {
            var jsonResponse = await GetRequest("/Category/GetCategories");
            var categories = JsonConvert.DeserializeObject<List<Category>>(jsonResponse);

            FrontPageVM frontPageVM = new FrontPageVM()
            {
                Categories = categories
            };
            return View(frontPageVM);
        }

        public async Task<IActionResult> CookiePolicy()
        {
            var json = await GetRequest("/Cookie/GetCookies");
            var cookies = JsonConvert.DeserializeObject<List<Cookie>>(json);
            return View(cookies);
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        //public IActionResult Feedback()
        //{
        //    return View();
        //}
        public IActionResult PrivacyPolicy()
        {
            return View();
        }
        public IActionResult TermsAndConditions()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> SendEmail(SimpleMail simpleMail)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if(simpleMail.Body == null)
        //        {
        //            return Json("Error");
        //        }
        //        else
        //        {
        //            try
        //            {
        //                await mailService.SendEmailAsync(simpleMail);
        //                return Json("Success");
        //            }
        //            catch (Exception e)
        //            {
        //                _logger.LogInformation(e.Message);
        //                return Json("Error");
        //            }
        //        }
        //    }
        //    return Json("Error");
        //}

        [HttpPost]
        public async Task<IActionResult> UpdateConsent(int functionalCookies, int statisticsCookies)
        {
            var json = await GetRequest("/Cookie/GetCookies");
            var cookies = JsonConvert.DeserializeObject<List<Cookie>>(json);
            Consent consent = new Consent();

            switch (functionalCookies)
            {
                case 1:
                    consent.Functional = true;
                    break;

                case 0:
                    foreach(var cookie in cookies)
                    {
                        if(cookie.Category == CookieCategory.Functional)
                        {
                            Response.Cookies.Delete(cookie.Name);
                        }
                    }
                    consent.Functional = false;
                    break;
            }
            switch (statisticsCookies)
            {
                case 1:
                    consent.Statistics = true;
                    break;

                case 0:
                    foreach (var cookie in cookies)
                    {
                        if (cookie.Category == CookieCategory.Statistics)
                        {
                            Response.Cookies.Delete(cookie.Name);
                        }
                    }
                    consent.Statistics = false;
                    break;
            }
            if(functionalCookies == 1 || statisticsCookies == 1)
            {
                consent.OnlyEssential = false;
            }
            else
            {
                consent.OnlyEssential = true;
            }
            consent.ShowBanner = false;
            
            HttpContext.Session.Set<Consent>(ConsentCookieKey, consent);

            ViewBag.Functional = consent.Functional;
            ViewBag.Statistics = consent.Statistics;
            return new PartialViewResult
            {
                ViewName = "_CookieSettingsPartial",
                ViewData = this.ViewData
            };
        }

        [HttpPost]
        public IActionResult AllowAllConsent()
        {
            var consent = new Consent
            {
                OnlyEssential = false,
                ShowBanner = false,
                Functional = true,
                Statistics = true
            };
            HttpContext.Session.Set<Consent>(ConsentCookieKey, consent);

            ViewBag.Functional = consent.Functional;
            ViewBag.Statistics = consent.Statistics;
            return new PartialViewResult
            {
                ViewName = "_CookieSettingsPartial",
                ViewData = this.ViewData
            };
        }

        public void Log(string message)
        {
            _logger.LogInformation(message);
        }

        [HttpPost]
        public async Task<IActionResult> OnlyEssentialConsent()
        {
            var json = await GetRequest("/Cookie/GetCookies");
            var cookies = JsonConvert.DeserializeObject<List<Cookie>>(json);
            foreach (var cookie in cookies)
            {
                if (cookie.Category == CookieCategory.Functional || cookie.Category == CookieCategory.Statistics)
                {
                    Response.Cookies.Delete(cookie.Name);
                }
            }

            var consent = new Consent
            {
                OnlyEssential = true,
                ShowBanner = false,
                Functional = false,
                Statistics = false
            };
            HttpContext.Session.Set<Consent>(ConsentCookieKey, consent);

            ViewBag.Functional = consent.Functional;
            ViewBag.Statistics = consent.Statistics;
            return new PartialViewResult
            {
                ViewName = "_CookieSettingsPartial",
                ViewData = this.ViewData
            };
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchString)
        {
            if (ModelState.IsValid)
            {
                if (searchString != null)
                {
                    var json = await GetRequest("/Product/GetProducts");
                    var searchList = new List<Product>();
                    foreach (var product in JsonConvert.DeserializeObject<List<Product>>(json))
                    {
                        if (product.Description.ToLower().Contains(searchString.ToLower()) || product.Category.Name.ToLower().Contains(searchString.ToLower()))
                        {
                            searchList.Add(product);
                        }
                    }
                    ViewBag.SearchProductList = searchList;
                }
                else
                {
                    ViewBag.SearchProductList = new List<Product>();
                }

                return new PartialViewResult
                {
                    ViewName = "_SearchPartial",
                    ViewData = this.ViewData
                };
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        public IActionResult UpdateCulture(string culture, string redirectController, string redirectAction)
        {
            if(culture == "Dansk")
            {
                culture = "da-DK";
            }
            else
            {
                culture = "en-GB";
            }

            try
            {
                if (Environment.IsDevelopment())
                {
                    Response.Cookies.Append(
                        "Culture",
                        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture, culture)),
                            new CookieOptions
                            {
                                Expires = DateTimeOffset.UtcNow.AddYears(1),
                                SameSite = SameSiteMode.Lax,
                                IsEssential = true,
                                HttpOnly = true,
                                Secure = true,
                                Domain = "localhost"

                            });
                }
                else
                {
                    Response.Cookies.Append(
                        "Culture",
                        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture, culture)),
                            new CookieOptions
                            {
                                Expires = DateTimeOffset.UtcNow.AddYears(1),
                                SameSite = SameSiteMode.Lax,
                                IsEssential = true,
                                HttpOnly = true,
                                Secure = true,
                                Domain = ".nykant.dk"

                            });
                }
            }
            catch(Exception e)
            {
                _logger.LogInformation(e.Message);
            }

            return RedirectToAction(redirectAction, redirectController);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewsSub(string email)
        {
            NewsSub newsSub = new NewsSub { Email = email };
            try
            {
                var test = new MailAddress(email);
                newsSub = _protectionService.ProtectNewsSub(newsSub);
                var response = await PostRequest("/NewsSub/Post", newsSub);

                if (response.IsSuccessStatusCode)
                {
                    return Json("Success");
                }
                else
                {
                    _logger.LogInformation($"{response.ReasonPhrase} - {response.StatusCode}");
                    return Json("Error");
                }
            }
            catch(Exception e)
            {
                _logger.LogInformation(e.Message);
                return Json("Error");
            }

        }
    }
}