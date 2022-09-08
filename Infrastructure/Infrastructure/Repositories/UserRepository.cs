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
    public class UserRepository : IUserRepository
    {
        private readonly MovieShopDbContext _dbContext;
        public UserRepository(MovieShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> AddUser(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<User> GetUserFavorites(int id)
        {
            var favorites = await _dbContext.Users.Include(f => f.FavoriteOfUser).ThenInclude(f => f.Movie)
                .FirstOrDefaultAsync(f => f.Id == id);
            return favorites;
        }

        public async Task<User> GetUserPurchases(int id)
        {
            var purchases = await _dbContext.Users.Include(u => u.PurchaseOfUser).ThenInclude(u => u.Movie)
                .FirstOrDefaultAsync(u => u.Id == id);
            return purchases;
        }

        public async Task<User> GetUserReviews(int id)
        {
            var reviews = await _dbContext.Users.Include(r => r.ReviewOfUser).ThenInclude(r => r.Movie)
                .FirstOrDefaultAsync(r => r.Id == id);
            return reviews;
        }
    }
}