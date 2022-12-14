using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructrue.Services
{
    public class GenreService: IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<List<GenreModel>> GetAllGenres()
        {
            var genres = await _genreRepository.GetAllGenres();
            var genresModels = genres.Select(g => new GenreModel { Id = g.Id, Name = g.Name}).ToList();
            return genresModels;
        }

        public async Task<bool> AddGenre(Genre genre)
        {
            var addGenre = await _genreRepository.AddGenre(genre);
            if (addGenre.Id > 0)
            {
                return true;
            }
            return false;
        }
    }
}
