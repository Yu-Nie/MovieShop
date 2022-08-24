using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services
{
    public interface IMovieService
    {
        // services will always return models/viewmodels/DTO (data transfer objects)
        List<MovieCardModel> GetTop30GrossingMovies();
    }
}
