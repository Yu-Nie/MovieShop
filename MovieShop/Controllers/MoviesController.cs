using Microsoft.AspNetCore.Mvc;

namespace MovieShop.Controllers
{
    public class MoviesController : Controller
    {
        // go to database and get the movie information by
        // movie by and send the data (Model) to view
        public IActionResult Details(int id)
        {
            return View();
        }
    }
}
