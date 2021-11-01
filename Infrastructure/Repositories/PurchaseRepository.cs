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
    public class PurchaseRepository : EfRepository<Purchase>,IPurchaseRepository
    {
        //public MovieShopDbContext _dbContext;
        public PurchaseRepository(MovieShopDbContext dbContext):base(dbContext)
        {
            //_dbContext = dbContext;
        }

        

        //public Task<IEnumerable<Purchase>> GetAllPurchases(int userId, int pageSize = 30, int pageIndex = 1)
        //{
        //    throw new NotImplementedException();
        //}



        //============================== Get Movies User Purchased ===================================//
        public async Task<IEnumerable<Purchase>> GetPurchasedByUserId(int id,int pageSize = 30, int pageIndex = 1)
        {
            var moviePurchased = await _dbContext.Purchases.Include(m=>m.Movie).Where(u => u.UserId == id).ToListAsync();
            return moviePurchased;
        }

        public async Task<Purchase> GetPurchaseDetails(int userId, int movieId)
        {
            var movie = await _dbContext.Purchases.Where(m => m.MovieId == movieId).Where(u => u.UserId == userId).FirstOrDefaultAsync();
            return movie;
        }

        /*

        ==================================== TODO =======================================
        public async Task<IEnumerable<Purchase>> GetAllPurchases(int pageSize = 30, int pageIndex = 1)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Purchase>> GetAllPurchasesForUser(int userId, int pageSize = 30, int pageIndex = 1)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Purchase>> GetAllPurchasesByMovie(int movieId, int pageSize = 30, int pageIndex = 1)
        {
            throw new System.NotImplementedException();
        }

        */

    }
}
