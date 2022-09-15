using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Infra;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICurrentUser _currentUser;

        public UserController(ICurrentUser currentUser, IUserService userService)
        {
            _currentUser = currentUser;
            _userService = userService;
        }

        [HttpGet]
        [Route("purchase")]

        public async Task<IActionResult> GetMoviesPurchasedByUser()
        {
            var purchases = await _userService.GetAllPurchasesdMovies(_currentUser.UserId);
            if (purchases == null)
            {
                return NotFound();
            }
            return Ok(purchases);
        }


        [HttpGet]
        [Route("movie-review")]
        public async Task<IActionResult> GetReviewsByUser()
        {
            var reviews = await _userService.GetAllReviewsByUser(_currentUser.UserId);
            if (reviews == null)
            {
                return NotFound();
            }
            return Ok(reviews);
        }

        [HttpGet]
        [Route("favorites")]
        public async Task<IActionResult> GetFavoritesByUser()
        {
            var favorites = await _userService.GetAllFavoriteMovies(_currentUser.UserId);
            if (favorites == null)
            {
                return NotFound();
            }
            return Ok(favorites);
        }
    }
}
