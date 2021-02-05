﻿using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NykantAPI.Data;
using NykantAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NykantAPI.Controllers
{
    public abstract partial class BaseController : ControllerBase
    {
        public readonly ApplicationDbContext _context;
        public readonly ILogger<BaseController> _logger;
        public BaseController(ILogger<BaseController> logger, ApplicationDbContext context)
        {

            _logger = logger;
            _context = context;
        }

        //public override ViewResult View()
        //{
        //    ViewBag.UserId = _userManager.GetUserId(User);
        //    return base.View();
        //}
        //public override ViewResult View(object model)
        //{
        //    ViewBag.UserId = _userManager.GetUserId(User);
        //    return base.View(model);
        //}
        //public override ViewResult View(string route, object model)
        //{
        //    ViewBag.UserId = _userManager.GetUserId(User);
        //    return base.View(route, model);
        //}
    }
}