using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IPurchaseRepository:IAsyncRepository<Purchase>
    {
        Task<IEnumerable<Purchase>> GetPurchasedByUserId(int id, int pageSize = 30, int pageIndex = 1);
        Task<Purchase> GetPurchaseDetails(int userId, int movieId);
        //Task<IEnumerable<Purchase>> GetAllPurchases(int userId,int pageSize = 30, int pageIndex = 1);

        //=================================TODO===================//

        //Task<IEnumerable<Purchase>> GetAllPurchasesByMovie(int movieId, int pageSize = 30, int pageIndex = 1);


    }
}
