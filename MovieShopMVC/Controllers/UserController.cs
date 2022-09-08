using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Infra;

namespace MovieShopMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ICurrentUser _currentUser;
        private readonly IUserService _userService;

        public UserController(ICurrentUser currentUser, IUserService userService)
        {
            _currentUser = currentUser;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Purchases()
        {
            // get all the movies purchased by user, user id
            // httpcontext.user.claims and then call the database and get the information to the view
            var moives = await _userService.GetAllPurchasesdMovies(_currentUser.UserId);
            return View(moives);
        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UserEditModel model)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BuyMovie(PurchaseMovieModel model)
        {
            await _userService.PurchaseMovies(model);
            return RedirectToAction("Purchases", "User", new {id = model.UseId});
        }

        [HttpPost]
        public async Task<IActionResult> FavoriteMovie()
        {
            
            return View();
        }


    }
}
