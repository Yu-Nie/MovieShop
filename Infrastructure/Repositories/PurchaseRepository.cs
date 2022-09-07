using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructrue.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly MovieShopDbContext _dbContext;
        public PurchaseRepository(MovieShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Purchase> AddPurchases(Purchase purchase)
        {
            _dbContext.Purchases.Add(purchase);
            await _dbContext.SaveChangesAsync();
            return purchase;
        }

        public async Task<Purchase> GetById(int movieId, int userId)
        {
            var purchase = await _dbContext.Purchases
                .Where(p => p.MovieId == movieId && p.UserId == userId)
                .Include(p => p.Movie).FirstOrDefaultAsync();
            return purchase;
        }
    }
}
