using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IGenreRepository
    {
        Task<List<Genre>> GetAllGenres();
        Task<Genre> AddGenre(Genre genre);
    }
}
