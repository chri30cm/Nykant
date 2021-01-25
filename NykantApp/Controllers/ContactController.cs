﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NykantApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NykantApp.Controllers
{
    public class ContactController : BaseController
    {
        public ContactController(ILogger<BaseController> logger) : base(logger)
        {
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
