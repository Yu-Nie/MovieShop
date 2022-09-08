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
    public class ReviewRepository : IReviewRepository
    {
        private readonly MovieShopDbContext _dbContext;

        public ReviewRepository(MovieShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Review> AddReview(Review review)
        {
            _dbContext.Reviews.Add(review);
            await _dbContext.SaveChangesAsync();
            return review;
        }

        public async Task<Review> DeleteReview(Review review)
        {
            _dbContext.Reviews.Remove(review);
            await _dbContext.SaveChangesAsync();
            return review;
        }

        public async Task<Review> GetById(int movieId, int userId)
        {
            var review = await _dbContext.Reviews.Where(x => x.MovieId == movieId && x.UserId == userId)
                .Include(x => x.Movie)
                .FirstOrDefaultAsync();
            return review;
        }

        public async Task<Review> EditReview(Review review)
        {
            var old = await GetById(review.MovieId, review.UserId);
            old.Rating = review.Rating;
            old.CreatedDate = review.CreatedDate;
            old.ReviewText = review.ReviewText;
            await _dbContext.SaveChangesAsync();
            return old;
        }
    }
}
