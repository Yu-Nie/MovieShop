using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IMovieRepository
    {
        // CRUD methods
        // get top 30 grossing movies movies from database

        //List<Movie> GetTop30GrossingMovies();
        Task<List<Movie>> GetTop30GrossingMovies();

        // Get Movie By Id

        // Movie GetById(int id);
        Task<Movie> GetById(int id);

        // Get Movie By Genre
        Task<PagedResultSet<Movie>> GetMoviesByGenrePagination(int genreId, int pageSize = 30, int page = 1);


    }
}