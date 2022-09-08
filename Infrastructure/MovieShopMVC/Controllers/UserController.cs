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
            // get all the movies the user likes by userid
            var movies = await _userService.GetAllFavoriteMovies(_currentUser.UserId);
            return View(movies);
        }

        [HttpGet]
        public async Task<IActionResult> Review()
        {
            // get all the reviews entered by userid
            var reviews = await _userService.GetAllReviewsByUser(_currentUser.UserId);
            return View(reviews);
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
            return RedirectToAction("Details", "Movies", new { id = model.MovieId });
        }

        [HttpPost]
        public async Task<IActionResult> FavoriteMovie(FavoriteMovieModel model)
        {
            await _userService.LikeMovie(model);
            return RedirectToAction("Details", "Movies", new { id = model.MovieId });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFavorite(FavoriteMovieModel model)
        {
            await _userService.UnLikeMovie(model);
            return RedirectToAction("Details", "Movies", new { id = model.MovieId });
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(ReviewMovieModel model)
        {
            await _userService.AddReview(model);
            return RedirectToAction("Details", "Movies", new { id = model.MovieId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReview(ReviewDetailsModel model)
        {
            await _userService.RemoveReview(model);
            return RedirectToAction("Details", "Movies", new { id = model.MovieId });
        }


        [HttpPost]
        public async Task<IActionResult> EditReview(ReviewMovieModel model)
        {
            await _userService.UpdateReview(model);
            return RedirectToAction("Details", "Movies", new { id = model.MovieId });
        }


    }
}
