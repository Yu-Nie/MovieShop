using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return View();
            }
            var userSuccess = await _accountService.ValidateUser(model);
            if (userSuccess == null)
            {
                return View(model);
            }

            // after successful auth
            // create claims: userID, firstName, lastName

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, userSuccess.Email),
                new Claim(ClaimTypes.NameIdentifier, userSuccess.Id.ToString()),
                new Claim(ClaimTypes.Surname, userSuccess.LastName),
                new Claim(ClaimTypes.GivenName, userSuccess.FirstName),
                new Claim("language", "english")
            };

            //identiy object
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // create cookie with some expiration time
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            // password matches
            // redirect to home page
            return LocalRedirect("~/");
        }

        public IActionResult Register()
        {
            // showing empty register view
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // save the database
                var userId = await _accountService.RegisterUser(model);

                if (userId > 0)
                {
                    return RedirectToAction("Login");
                }

            }
            return View(model);

        }
    }
}
