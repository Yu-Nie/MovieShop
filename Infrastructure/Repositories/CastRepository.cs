using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructrue.Repositories
{
    
    public class CastRepository
    {
        private readonly MovieShopDbContext _movieShopDbContext;
        public CastRepository(MovieShopDbContext dbContext)
        {
            _movieShopDbContext = dbContext;
        }
        public async Task<Cast> GetById(int id)
        {
            // select * from movie where id = 1 join  genre, cast, moviegenre, moviecast
            var castDetails = await _movieShopDbContext.Casts.FirstOrDefaultAsync(m => m.Id == id);
            return castDetails;
        }
    }
   
}
