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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IReviewRepository _reviewRepository;

        public UserService(IUserRepository userRepository, IMovieRepository movieRepository, IPurchaseRepository purchaseRepository, IFavoriteRepository favoriteRepository, IReviewRepository reviewRepository)
        {
            _userRepository = userRepository;
            _movieRepository = movieRepository;
            _purchaseRepository = purchaseRepository;
            _favoriteRepository = favoriteRepository;
            _reviewRepository = reviewRepository;
        }

        // review functions
        public async Task<List<ReviewDetailsModel>> GetAllReviewsByUser(int userId)
        {
            var reviews = await _userRepository.GetUserReviews(userId);
            var reviewDetails = new List<ReviewDetailsModel>();
            foreach(var review in reviews.ReviewOfUser)
            {
                reviewDetails.Add(new ReviewDetailsModel
                {
                    MovieId = review.MovieId,
                    UserId = review.UserId,
                    ReviewText = review.ReviewText,
                    Rating = review.Rating,
                    CreatedDate = review.CreatedDate
                });
            }
            return reviewDetails;
        }

        public async Task<bool> AddReview(ReviewMovieModel model)
        {
            Review review = new Review{
                MovieId = model.MovieId,
                UserId = model.UserId,
                ReviewText = model.ReviewText,
                Rating = model.Rating,
                CreatedDate = model.CreatedDate
            };
            await _reviewRepository.AddReview(review);
            return true;

        }

        public async Task<bool> RemoveReview(ReviewDetailsModel model)
        {
            Review review = await _reviewRepository.GetById(model.MovieId, model.UserId);
            await _reviewRepository.DeleteReview(review);
            return true;
        }

        public async Task<bool> ReviewExits(int movieId, int userId)
        {
            Review review = await _reviewRepository.GetById(movieId, userId);
            if (review == null) return false;
            else return true;
        }

        public async Task<bool> UpdateReview(ReviewMovieModel model)
        {
            Review review = new Review
            {
                MovieId = model.MovieId,
                UserId = model.UserId,
                Rating = model.Rating,
                ReviewText = model.ReviewText,
                CreatedDate = model.CreatedDate,
            };
            await _reviewRepository.EditReview(review);
            return true;
        }

        public async Task<ReviewDetailsModel> GetReviewDetails(int movieId, int userId)
        {
            var review = await _reviewRepository.GetById(movieId, userId);
            var reviewDetail = new ReviewDetailsModel
            {
                MovieId = movieId,
                UserId = userId,
                Rating = review.Rating,
                ReviewText = review.ReviewText,
                CreatedDate = review.CreatedDate,
            };
            return reviewDetail;
        }


        // favorite functions
        public async Task<List<MovieCardModel>> GetAllFavoriteMovies(int userId)
        {
            var movies = new List<MovieCardModel>();
            var favorites = await _userRepository.GetUserFavorites(userId);
            foreach(var movie in favorites.FavoriteOfUser)
            {
                movies.Add(new MovieCardModel
                {
                    Id = movie.MovieId,
                    Title = movie.Movie.Title,
                    PosterUrl = movie.Movie.PosterUrl,
                });
            }

            return movies;
        }

        public async Task<bool> LikeMovie(FavoriteMovieModel favoriteMovie)
        {
            Favorite like = new Favorite
            {
                MovieId = favoriteMovie.MovieId,
                UserId = favoriteMovie.UserId,
            };

            await _favoriteRepository.AddFavorite(like);
            return true;
        }

        public async Task<bool> UnLikeMovie(FavoriteMovieModel favoriteMovie)
        {
            var movie = await _favoriteRepository.GetbyId(favoriteMovie.MovieId, favoriteMovie.UserId);
            await _favoriteRepository.RemoveFavorite(movie);
            return true;
        }

        public async Task<bool> Liked(int movieId, int userId)
        {
            var movie = await _favoriteRepository.GetbyId(movieId, userId);
            if (movie == null) return false;
            else return true;
        }


        // purchase functions
        public async Task<List<MovieCardModel>> GetAllPurchasesdMovies(int userId)
        {
            var movies = new List<MovieCardModel>();
            var purchases = await _userRepository.GetUserPurchases(userId);
            foreach(var purchase in purchases.PurchaseOfUser)
            {
                movies.Add(new MovieCardModel
                {
                    Id = purchase.MovieId,
                    Title = purchase.Movie.Title,
                    PosterUrl = purchase.Movie.PosterUrl
                });
            }
            return movies;
        }

        public async Task<PurchaseDetailsModel> GetPurchaseDetails(int movieId, int userId)
        {
            var purchase = await _purchaseRepository.GetById(movieId, userId);
            var purchaseDetails = new PurchaseDetailsModel
            {
                MovieId = purchase.MovieId,
                UserId = purchase.UserId,
                PosterUrl = purchase.Movie.PosterUrl,
                TotalPrice = purchase.TotalPrice,
                PurchasedDate = purchase.PurchaseDateTime,
                PurchaseNumber = purchase.PurchaseNumber,
            };
            return purchaseDetails;
        }

        public async Task<bool> IsPurchased(int movieId, int userId)
        {
            var purchase = await _purchaseRepository.GetById(movieId, userId);
            if (purchase == null) return false;
            else return true;
        }

        public async Task<bool> PurchaseMovies(PurchaseMovieModel purchaseMovie)
        {
            var movie = await _movieRepository.GetById(purchaseMovie.MovieId);
            Purchase dbPurchase = new Purchase
            {
                MovieId = purchaseMovie.MovieId,
                UserId = purchaseMovie.UserId,
                TotalPrice = movie.Price.GetValueOrDefault(),
                PurchaseNumber = purchaseMovie.PurchaseNumber,
                PurchaseDateTime = purchaseMovie.PurchaseDateTime,
            };

            var createPurchase = await _purchaseRepository.AddPurchases(dbPurchase);

            if (createPurchase.PurchaseNumber == Guid.Empty) return false;
            else return true;
        }

    }
}
