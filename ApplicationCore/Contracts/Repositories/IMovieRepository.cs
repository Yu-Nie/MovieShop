using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IMovieRepository
    {
        // get top 30 grossing movies from database

        List<Movie> GetTop30GrossingMovies();
        // get movie by id

        // get movie by genre
    }
}
