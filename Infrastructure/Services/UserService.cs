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

        public UserService(IUserRepository userRepository, IMovieRepository movieRepository, IPurchaseRepository purchaseRepository)
        {
            _userRepository = userRepository;
            _movieRepository = movieRepository;
            _purchaseRepository = purchaseRepository;
        }

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
                PurchasedDate = purchase.PurchaseDateTime
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
                UserId = purchaseMovie.UseId,
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
