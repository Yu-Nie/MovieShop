using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IPurchaseRepository
    {
        Task<Purchase> AddPurchases(Purchase purchase);
        Task<Purchase> GetById(int movieId, int userId);
    }
}
