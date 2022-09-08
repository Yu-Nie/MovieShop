using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services
{
    public interface IUserService
    {
        Task<bool> PurchaseMovies(PurchaseMovieModel purchaseMovie);
        Task<bool> IsPurchased(int movieId, int userId);
        Task<List<MovieCardModel>> GetAllPurchasesdMovies(int userId);
        Task<PurchaseDetailsModel> GetPurchaseDetails(int movieId, int userId);


        Task<List<MovieCardModel>> GetAllFavoriteMovies(int userId);
        Task<bool> LikeMovie(FavoriteMovieModel favoriteMovie);
        Task<bool> UnLikeMovie(FavoriteMovieModel favoriteMovie);
        Task<bool> Liked(int movieId, int userId);
    }
}
