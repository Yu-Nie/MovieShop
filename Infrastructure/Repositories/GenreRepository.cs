using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GenreRepository: IGenreRepository
    {
        private readonly MovieShopDbContext _movieShopDbContext;

        public GenreRepository(MovieShopDbContext movieShopDbContext)
        {
            _movieShopDbContext = movieShopDbContext;
        }

        public async Task<List<Genre>> GetAllGenres()
        {
            var genres = await _movieShopDbContext.Genres.ToListAsync();
            return genres;
        }

        public async Task<Genre> AddGenre(Genre genre)
        {
            _movieShopDbContext.Genres.Add(genre);
            await _movieShopDbContext.SaveChangesAsync();
            return genre;
        }

    }
}
