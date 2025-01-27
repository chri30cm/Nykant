﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using NykantIS.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using NykantIS.Models.ViewModels;
using NykantIS.Data;
using System.Security.Claims;
using Serilog;
using NykantIS.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace NykantIS.Controllers
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;
        private readonly IConfiguration configuration;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events,
            IWebHostEnvironment webHostEnvironment,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _events = events;
            this.webHostEnvironment = webHostEnvironment;
            this.configuration = configuration;
        }

        /// <summary>
        /// Entry point into the login workflow
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Login(string redirectaction = null, string redirectcontroller = null, string returnUrl = null)
        {
            if (redirectaction == "Details")
            {
                redirectaction = "Index";
            }
            // build a model so we know what to show on the login page
            var vm = await BuildLoginViewModelAsync(returnUrl, redirectaction, redirectcontroller);

            if (vm.IsExternalLoginOnly)
            {
                // we only have one option for logging in and it's an external provider
                return RedirectToAction("Challenge", "External", new { scheme = vm.ExternalLoginScheme, returnUrl });
            }
            return View(vm);
        }

        /// <summary>
        /// Handle postback from username/password login
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel model, string button)
        {
            // check if we are in the context of an authorization request
            var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (_signInManager.IsSignedIn(User))
                {
                    if (model.RedirectController == null || model.RedirectAction == null)
                    {
                        return Redirect($"{model.BasePath}/account/login");
                    }
                    return Redirect($"{model.BasePath}/account/login?redirectcontroller={model.RedirectController}&redirectaction={model.RedirectAction}");
                }
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberLogin, lockoutOnFailure: true);
                    if (result.Succeeded)
                    {
                        await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName, clientId: context?.Client.ClientId));

                        if (context != null)
                        {
                            if (context.IsNativeClient())
                            {
                                // The client is native, so this change in how to
                                // return the response is for better UX for the end user.
                                return this.LoadingPage("Redirect", $"{model.BasePath}/account/login?redirectcontroller={model.RedirectController}&redirectaction={model.RedirectAction}");
                            }

                            if (model.RedirectController == null || model.RedirectAction == null)
                            {
                                return Redirect($"{model.BasePath}/account/login");
                            }
                            return Redirect($"{model.BasePath}/account/login?redirectcontroller={model.RedirectController}&redirectaction={model.RedirectAction}");
                        }

                        // request for a local page
                        if (Url.IsLocalUrl(model.ReturnUrl))
                        {
                            if (model.RedirectController == null || model.RedirectAction == null)
                            {
                                return Redirect($"{model.BasePath}/account/login");
                            }
                            return Redirect($"{model.BasePath}/account/login?redirectcontroller={model.RedirectController}&redirectaction={model.RedirectAction}");
                        }
                        else if (string.IsNullOrEmpty(model.ReturnUrl))
                        {
                            if (model.RedirectController == null || model.RedirectAction == null)
                            {
                                return Redirect($"{model.BasePath}/account/login");
                            }
                            return Redirect($"{model.BasePath}/account/login?redirectcontroller={model.RedirectController}&redirectaction={model.RedirectAction}");
                        }
                        else
                        {
                            // user might have clicked on a malicious link - should be logged
                            throw new Exception("invalid return URL");
                        }
                    }
                    else if (result.RequiresTwoFactor)
                    {
                        return Redirect($"{model.ISPath}/is/identity/account/loginwith2fa?rememberMe={model.RememberLogin}");
                    }
                    else if (result.IsLockedOut)
                    {
                        return RedirectToPage("./Lockout");
                    }
                }

                await _events.RaiseAsync(new UserLoginFailureEvent(model.Email, "invalid credentials", clientId: context?.Client.ClientId));
                ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentialsErrorMessage);
            }

            // something went wrong, show form with error
            var vm = await BuildLoginViewModelAsync(model);
            return View(vm);
        }


        /// <summary>
        /// Show logout page
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            // build a model so the logout page knows what to display
            var vm = await BuildLogoutViewModelAsync(logoutId);

            if (vm.ShowLogoutPrompt == false)
            {
                // if the request for logout was properly authenticated from IdentityServer, then
                // we don't need to show the prompt and can just log the user out directly.
                return await Logout(vm);
            }

            return View(vm);
        }

        /// <summary>
        /// Handle logout page postback
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutInputModel model)
        {
            // build a model so the logged out page knows what to display
            var vm = await BuildLoggedOutViewModelAsync(model.LogoutId);

            if (User?.Identity.IsAuthenticated == true)
            {
                // delete local authentication cookie
                await _signInManager.SignOutAsync();

                // raise the logout event
                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }

            // check if we need to trigger sign-out at an upstream identity provider
            if (vm.TriggerExternalSignout)
            {
                // build a return URL so the upstream provider will redirect back
                // to us after the user has logged out. this allows us to then
                // complete our single sign-out processing.
                string url = Url.Action("Logout", new { logoutId = vm.LogoutId });

                // this triggers a redirect to the external provider for sign-out
                return SignOut(new AuthenticationProperties { RedirectUri = url }, vm.ExternalAuthenticationScheme);
            }

            return Redirect($"{configuration.GetSection("Urls")["mvc"]}");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        /*****************************************/
        /* helper APIs for the AccountController */
        /*****************************************/
        private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl, string redirectAction, string redirectController)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            if (context?.IdP != null && await _schemeProvider.GetSchemeAsync(context.IdP) != null)
            {
                var local = context.IdP == IdentityServer4.IdentityServerConstants.LocalIdentityProvider;

                // this is meant to short circuit the UI and only trigger the one external IdP
                var vm = new LoginViewModel
                {
                    EnableLocalLogin = local,
                    ReturnUrl = returnUrl,
                    Email = context?.LoginHint,
                    RedirectAction = redirectAction,
                    RedirectController = redirectController,
                    BasePath = $"{configuration.GetSection("Urls")["mvc"]}",
                    ISPath = $"{configuration.GetSection("Urls")["is"]}"
                };

                if (!local)
                {
                    vm.ExternalProviders = new[] { new ExternalProvider { AuthenticationScheme = context.IdP } };
                }

                return vm;
            }

            var schemes = await _schemeProvider.GetAllSchemesAsync();

            var providers = schemes
                .Where(x => x.DisplayName != null)
                .Select(x => new ExternalProvider
                {
                    DisplayName = x.DisplayName ?? x.Name,
                    AuthenticationScheme = x.Name
                }).ToList();

            var allowLocal = true;
            if (context?.Client.ClientId != null)
            {
                var client = await _clientStore.FindEnabledClientByIdAsync(context.Client.ClientId);
                if (client != null)
                {
                    allowLocal = client.EnableLocalLogin;

                    if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
                    {
                        providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
                    }
                }
            }

            return new LoginViewModel
            {
                AllowRememberLogin = AccountOptions.AllowRememberLogin,
                EnableLocalLogin = allowLocal && AccountOptions.AllowLocalLogin,
                ReturnUrl = returnUrl,
                Email = context?.LoginHint,
                ExternalProviders = providers.ToArray(),
                RedirectAction = redirectAction,
                RedirectController = redirectController,
                BasePath = $"{configuration.GetSection("Urls")["mvc"]}"
            };
        }

        private async Task<LoginViewModel> BuildLoginViewModelAsync(LoginInputModel model)
        {
            var vm = await BuildLoginViewModelAsync(model.ReturnUrl, model.RedirectAction, model.RedirectController);
            vm.Email = model.Email;
            vm.RememberLogin = model.RememberLogin;
            return vm;
        }

        private async Task<LogoutViewModel> BuildLogoutViewModelAsync(string logoutId)
        {
            var vm = new LogoutViewModel { LogoutId = logoutId, ShowLogoutPrompt = AccountOptions.ShowLogoutPrompt };

            if (User?.Identity.IsAuthenticated != true)
            {
                // if the user is not authenticated, then just show logged out page
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            var context = await _interaction.GetLogoutContextAsync(logoutId);
            if (context?.ShowSignoutPrompt == false)
            {
                // it's safe to automatically sign-out
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            // show the logout prompt. this prevents attacks where the user
            // is automatically signed out by another malicious web page.
            return vm;
        }

        private async Task<LoggedOutViewModel> BuildLoggedOutViewModelAsync(string logoutId)
        {
            // get context information (client name, post logout redirect URI and iframe for federated signout)
            var logout = await _interaction.GetLogoutContextAsync(logoutId);

            var vm = new LoggedOutViewModel
            {
                AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
                PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
                ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout?.ClientName,
                SignOutIframeUrl = logout?.SignOutIFrameUrl,
                LogoutId = logoutId
            };

            if (User?.Identity.IsAuthenticated == true)
            {
                var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
                if (idp != null && idp != IdentityServer4.IdentityServerConstants.LocalIdentityProvider)
                {
                    var providerSupportsSignout = await HttpContext.GetSchemeSupportsSignOutAsync(idp);
                    if (providerSupportsSignout)
                    {
                        if (vm.LogoutId == null)
                        {
                            // if there's no current logout context, we need to create one
                            // this captures necessary info from the current logged in user
                            // before we signout and redirect away to the external IdP for signout
                            vm.LogoutId = await _interaction.CreateLogoutContextAsync();
                        }

                        vm.ExternalAuthenticationScheme = idp;
                    }
                }
            }

            return vm;
        }
    }
}