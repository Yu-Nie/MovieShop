using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructrue.Services
{
    public class CastService : ICastService
    {
        private readonly ICastRepository _castRepository;

        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }

        public async Task<CastDetailsModel> GetCastDetails(int id)
        {
            var castDetail = await _castRepository.GetById(id);

            var castDetailsModel = new CastDetailsModel
            {
                Id = castDetail.Id,
                Name = castDetail.Name,
                ProfilePath = castDetail.ProfilePath,
                Gender = castDetail.Gender,
                TmdbUrl = castDetail.TmdbUrl
            };

            foreach(var movie in castDetail.MoviesOfCast)
            {
                castDetailsModel.Movies.Add(new MovieDetailsModel
                {
                    Id = movie.MovieId,
                    Title = movie.Movie.Title,
                    PosterUrl = movie.Movie.PosterUrl,
                });
            }

            foreach(var movieCast in castDetail.MoviesOfCast)
            {
                castDetailsModel.Casts.Add(new MovieCastModel
                {
                    Character = movieCast.Character
                });
            }

            return castDetailsModel;
        }
    }
}
