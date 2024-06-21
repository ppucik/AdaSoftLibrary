using AdaSoftLibrary.Application.Common.Interfaces;
using AdaSoftLibrary.Domain.Authentication;
using AdaSoftLibrary.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AdaSoftLibrary.Web.Controllers
{
    public class AccessController : Controller
    {
        public const string NAME = "Access";
        public const string ACTION_LOGIN = nameof(Login);
        public const string ACTION_LOGOUT = nameof(Logout);

        private readonly IUserAuthenticationService _userAuthenticationService;
        private readonly ILogger<HomeController> _logger;

        public AccessController(IUserAuthenticationService userAuthenticationService, ILogger<HomeController> logger)
        {
            _userAuthenticationService = userAuthenticationService;
            _logger = logger;
        }

        public IActionResult Login()
        {
            if (HttpContext.User.Identity?.IsAuthenticated ?? true)
                return RedirectToAction(HomeController.ACTION_INDEX, HomeController.NAME);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel model)
        {
            ViewData["ValidateMessage"] = string.Empty;

            var authenticationRequest = new AuthenticationRequest
            {
                UserName = model.UserName,
                Password = model.Password
            };

            var authenticationResponse = await _userAuthenticationService.AuthenticateAsync(authenticationRequest);

            if (authenticationResponse.IsVerified)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim(ClaimTypes.Role, "Administrator"),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    // Refreshing the authentication session should be allowed.

                    IsPersistent = model.RememberMe,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                _logger.LogInformation($"User {model.UserName} logged in at {DateTime.Now}.");

                return RedirectToAction(HomeController.ACTION_INDEX, HomeController.NAME);
            }

            ViewData["ValidateMessage"] = "Nesprávne prihlasovacie údaje";

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            // Clear the existing external cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(HomeController.ACTION_INDEX, HomeController.NAME);
        }
    }
}
