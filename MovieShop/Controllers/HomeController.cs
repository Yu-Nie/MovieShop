using Infrastructrue.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShop.Models;
using System.Diagnostics;

namespace MovieShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Controller (c# class that derive Controller base class)
        // Controller ==> Sevices ==> Repositories ==> Database using EF Core or Dapper or both
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Action methods
        // typically returns view()
        // index view is the default
        public IActionResult Index()
        {
            // got to database and get the data

            var movieService = new MovieService();
            var movies = movieService.GetTop30GrossingMovies();
            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}