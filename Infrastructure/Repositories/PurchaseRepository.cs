using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        public MovieShopDbContext _dbContext;
        public PurchaseRepository(MovieShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Purchase>> GetPurchasedByUserId(int id)
        {
            var moviePurchased = await _dbContext.Purchases.Include(m=>m.Movie).Where(u => u.UserId == id).ToListAsync();
            return moviePurchased;

        }

    }
}
