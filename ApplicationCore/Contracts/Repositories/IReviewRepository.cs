using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IReviewRepository
    {
        Task<Review> AddReview(Review review);
        Task<Review> EditReview(Review review);
        Task<Review> DeleteReview(Review review);
        Task<Review> GetById(int movieId, int userId);
    }
}
