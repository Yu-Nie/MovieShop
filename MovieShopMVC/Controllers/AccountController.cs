using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            var userSuccess = _accountService.ValidateUser(model);
            if(userSuccess.Id > 0)
            {
                // password matches
                // redirect to home page
                return LocalRedirect("~/");
            }
            return View();
        }

        public IActionResult Register()
        {
            // showing empty register view
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            var userId = await _accountService.RegisterUser(model);
            
            if(userId > 0)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}
