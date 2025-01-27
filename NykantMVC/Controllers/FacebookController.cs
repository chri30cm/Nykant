﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NykantMVC.Extensions;
using NykantMVC.Models;
using NykantMVC.Models.Facebook;
using NykantMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace NykantMVC.Controllers
{
    //[AutoValidateAntiforgeryToken]
    public class FacebookController : BaseController
    {
        public FacebookController(ILogger<FacebookController> logger, IOptions<Urls> urls, HtmlEncoder htmlEncoder, IConfiguration conf, ITokenService _tokenService) : base(logger, urls, htmlEncoder, conf, _tokenService)
        { }


        [Authorize(Roles = "Admin,Raffler")]
        [HttpGet]
        public IActionResult Posts()
        {
                return View();
        }

        [Authorize(Roles = "Admin,Raffler")]
        [HttpGet("/Facebook/Post/{postId}")]
        public async Task<IActionResult> Post(string postId)
        {
            try
            {
                var facebookSession = HttpContext.Session.Get<FacebookSession>(FacebookSessionKey);
                if (facebookSession.Feed != null)
                {
                    for (int i = 0; i < facebookSession.Feed.Posts.Count(); i++)
                    {
                        if (facebookSession.Feed.Posts[i].Id == postId)
                        {
                            facebookSession.Feed.Posts[i].Request = facebookSession.Feed.Request;
                            facebookSession.Feed.Posts[i].Json = facebookSession.Feed.Json;
                            facebookSession.Feed.Posts[i].Winner = new Winner
                            {
                                Name = "",
                                Id = ""
                            };
                            Likes likes = await FacebookGetPostLikes(facebookSession.AccessToken, postId);
                            facebookSession.Feed.Posts[i].Likes = likes;
                            Comments comments = await FacebookGetPostComments(facebookSession.AccessToken, postId);
                            facebookSession.Feed.Posts[i].Comments = comments;
                            HttpContext.Session.Set<FacebookSession>(FacebookSessionKey, facebookSession);
                            return View(facebookSession.Feed.Posts[i]);
                        }
                    }
                }
                return View(new Post());
            }
            catch(Exception e)
            {
                _logger.LogError($"time: {DateTime.Now} - {e.Message}, {e.InnerException}, {e.StackTrace}, {e.TargetSite}");
                return View(new Post());
            }

        }

        [Authorize(Roles = "Admin,Raffler")]
        [Route("/Facebook/PostFeed")]
        [HttpPost]
        public async Task<IActionResult> PostFeed(string jsonFeed, string accessToken)
        {
            try
            {
                _logger.LogInformation("PostFeed Hit");
                var feed = JsonConvert.DeserializeObject<Feed>(jsonFeed);
                feed.Request = "https://graph.facebook.com/v13.0/104882272120980/feed?fields=from,id,created_time,comments&access_token=" + accessToken;
                feed.Json = jsonFeed;
                FacebookSession facebookSession = new FacebookSession
                {
                    Feed = feed,
                    AccessToken = accessToken
                };
                HttpContext.Session.Set<FacebookSession>(FacebookSessionKey, facebookSession);

                ViewData.Model = feed;
                return new PartialViewResult
                {
                    ViewName = "/Views/Facebook/_PostsPartial.cshtml",
                    ViewData = this.ViewData
                };
            }
            catch (Exception e)
            {
                _logger.LogError($"time: {DateTime.Now} - {e.Message}, {e.InnerException}, {e.StackTrace}, {e.TargetSite}");
                return BadRequest();
            }

        }

        [Authorize(Roles = "Admin,Raffler")]
        [HttpPost("/Facebook/Post/{postId}")]
        public IActionResult PickRandom(string postId)
        {
            try
            {
                var facebookSession = HttpContext.Session.Get<FacebookSession>(FacebookSessionKey);
                Post post = facebookSession.Feed.Posts.Find(x => x.Id == postId);
                if (post.Comments != null && post.Likes != null)
                {
                    var random = new Random();
                    var comments = post.Comments.List;
                    bool found = false;
                    for (int j = 0; j < 20; j++)
                    {
                        int i = random.Next(comments.Count);
                        foreach (var like in post.Likes.List)
                        {
                            if (like.Name == comments[i].From.Name)
                            {
                                found = true;
                                post.Winner.Name = comments[i].From.Name;
                                post.Winner.Id = comments[i].From.Id;
                                break;
                            }
                        }
                        if (found)
                            break;
                    }
                }

                return View("Post", post);
            }
            catch (Exception e)
            {
                _logger.LogError($"time: {DateTime.Now} - {e.Message}, {e.InnerException}, {e.StackTrace}, {e.TargetSite}");
                return BadRequest();
            }

        }
    }
}
