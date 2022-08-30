using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {

            // go to database and get the movie information by
            // movie id and send the data (Model) to the view
            // ADO.NET 
            // Dapper Stackoverflow -> Micro ORM
            // Entity Framework Core => Full ORM

            // Code is Maintenable, Reusable, Readable, extensible, testable
            // layers => Layered architecture
            // Onion, Clean 
            var movieDetails = await _movieService.GetMovieDetails(id);
            return View(movieDetails);
        }
    }
}