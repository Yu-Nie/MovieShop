using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [Route("top-grossing")]
        // Attribute Routing
        // MVC http://localhost/movies/GetTop30GrossingMovies => traditional/convetion based routing
        // http://localhost/api/movies/GetTop30GrossingMovies
        public async Task<IActionResult> GetTop30GrossingMovies()
        {
            // call my service
            var movies = await _movieService.GetTop30GrossingMovies();

            // return the movies information in JSON format and HTTP status code
            // ASP.NET Core automatically serializes C# objects to JSON objections

            if (movies == null || !movies.Any())
            {
                return NotFound(new {errorMessage = "No Movies Found"});
            }
            return Ok(movies);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);
            if(movie == null)
            {
                return NotFound(new { errorMessage = $"No Movie Found for {id}" });
            }
            return Ok(movie);
        }

        [HttpGet]
        [Route("genre/{genreId:int}")]
        public async Task<IActionResult> GetMovieByGenre(int id)
        {
            var movies = await _movieService.GetMoviesByGenrePagination(id);
            if(movies == null)
            {
                return NotFound(new { errorMessage = $"No Movie Found for {id}" });
            }
            return Ok(movies);
        }
    }
}
