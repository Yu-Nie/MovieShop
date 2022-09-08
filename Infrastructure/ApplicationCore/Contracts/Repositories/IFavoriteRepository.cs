using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IFavoriteRepository
    {
        Task<Favorite> AddFavorite(Favorite favorite);
        Task<Favorite> RemoveFavorite(Favorite favorite);
        Task<Favorite> GetbyId(int movieId, int userId);
    }
}
