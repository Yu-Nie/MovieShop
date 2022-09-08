using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FavoriteRepository: IFavoriteRepository
    {
        private readonly MovieShopDbContext _dbContext;
        public FavoriteRepository(MovieShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Favorite> AddFavorite(Favorite favorite)
        {
            _dbContext.Favorites.Add(favorite);
            await _dbContext.SaveChangesAsync();
            return favorite;
        }

        public async Task<Favorite> GetbyId(int movieId, int userId)
        {
            var favorite = await _dbContext.Favorites.Where(f => f.MovieId == movieId && f.UserId == userId)
                .Include(f => f.Movie).FirstOrDefaultAsync();
            return favorite;
        }

        public async Task<Favorite> RemoveFavorite(Favorite favorite)
        {
            _dbContext.Favorites.Remove(favorite);
            await _dbContext.SaveChangesAsync();
            return favorite;
        }
    }
}
