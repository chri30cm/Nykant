﻿//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using NykantApp.Models;
//using NykantApp.ViewModels;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace NykantApp.Controllers
//{
//    public class CheckoutController : BaseController
//    {
//        public CheckoutController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ILogger<BaseController> logger) : base(signInManager, userManager, logger)
//        {
//        }

//        public IActionResult CustomerInfo(string bagId, int priceSum)
//        {
//            var bagItems = _context.BagItems
//                .Include(b => b.Bag)
//                .Include(b => b.Product)
//                .Where(x => x.BagId == bagId);

//            CheckoutVM checkoutVM = new CheckoutVM
//            {
//                PriceSum = priceSum,
//                BagItems = bagItems
//            };

//            return View(checkoutVM);
//        }

//        [HttpPost]
//        public IActionResult CustomerInfo(CheckoutVM checkoutVM)
//        {
//            CustomerInfo shipping = checkoutVM.CustomerInfo;
//            if (_signInManager.IsSignedIn(User))
//            {
//                shipping.UserId = _userManager.GetUserId(User);
//            }
//            _context.CustomerInfos.Add(shipping);
//            _context.SaveChanges();
//            checkoutVM.CustomerInfo = shipping;
            
//            return RedirectToAction("Payment", checkoutVM);
//        }

//        public IActionResult Payment(CheckoutVM checkoutVM)
//        {

//            return View(checkoutVM);
//        }
//    }
//}