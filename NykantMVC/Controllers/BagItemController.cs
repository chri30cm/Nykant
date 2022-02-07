﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NykantMVC.Extensions;
using NykantMVC.Models;
using NykantMVC.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace NykantMVC.Controllers
{
    [AutoValidateAntiforgeryToken]
    [AllowAnonymous]

    public class BagItemController : BaseController
    {

        public BagItemController(ILogger<BaseController> logger, IOptions<Urls> urls, HtmlEncoder htmlEncoder) : base(logger, urls, htmlEncoder)
        {
        }

        

        [HttpPost]
        public async Task<IActionResult> PostBagItem(int id, int quantity)
        {
            bool isAuthenticated = User.Identity.IsAuthenticated;
            int bagItemQuantity = HttpContext.Session.Get<int>(BagItemAmountKey);

            BagItem bagItem = new BagItem
            {
                ProductId = id,
                Quantity = quantity
            };

            if (!isAuthenticated)
            {
                try
                {
                    var json = await GetRequest($"/Product/GetProduct/{id}");
                    Product product = JsonConvert.DeserializeObject<Product>(json);
                    bagItem.Product = product;

                    List<BagItem> bagItems = HttpContext.Session.Get<List<BagItem>>(BagSessionKey);
                    if (bagItems == default || bagItems == null)
                    {
                        bagItems = new List<BagItem>();
                    }

                    bool bagItemExists = false;
                    foreach (var item in bagItems)
                    {
                        if (item.ProductId == bagItem.ProductId)
                        {
                            bagItemExists = true;
                            item.Quantity += bagItem.Quantity;
                        }
                    }
                    if (!bagItemExists)
                    {
                        bagItems.Add(bagItem);
                        bagItemQuantity += 1;
                        HttpContext.Session.Set<int>(BagItemAmountKey, bagItemQuantity);
                    }

                    HttpContext.Session.Set<List<BagItem>>(BagSessionKey, bagItems);

                    ViewBag.ProductQuantity = bagItem.Quantity;
                    ViewData.Model = bagItem.Product;
                    return new PartialViewResult
                    {
                        ViewName = "/Views/Product/_ItemAddedPartial.cshtml",
                        ViewData = this.ViewData
                    };
                }
                catch (Exception e)
                {
                    return Content(e.Message);
                }
            }
            else
            {
                bagItem.Subject = User.Claims.FirstOrDefault(x => x.Type == "sub").Value;

                var response = await PostRequest("/BagItem/PostBagItem", bagItem);
                if (response.IsSuccessStatusCode)
                {
                    bagItemQuantity += 1;
                    HttpContext.Session.Set<int>(BagItemAmountKey, bagItemQuantity);

                    ViewBag.ProductQuantity = bagItem.Quantity;
                    ViewData.Model = bagItem.Product;
                    return new PartialViewResult
                    {
                        ViewName = "/Views/Product/_ItemAddedPartial.cshtml",
                        ViewData = this.ViewData
                    };
                }
                else
                {
                    return Content("Fejl, prøv igen.");
                }
            }
        }

        private bool BagItemExists(List<BagItem> list, BagItem bagItem)
        {
            foreach (var item in list)
            {
                if (item.ProductId == bagItem.ProductId && item.Subject == bagItem.Subject)
                {
                    return true;
                }
            }
            return false;
        }
        private BagItem GetBagItem(List<BagItem> list, BagItem bagItem)
        {
            foreach (var item in list)
            {
                if (item.ProductId == bagItem.ProductId && item.Subject == bagItem.Subject)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
