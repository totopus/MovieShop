using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CastRepository : EfRepository<Cast>, ICastRepository
    {
        public CastRepository(MovieShopDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Cast> GetCastDetails(int id)
        {
            var cast = await _dbContext.Casts.Include(m => m.Movies).ThenInclude(m=>m.Movie).FirstOrDefaultAsync(d=>d.Id==id);
            return cast;
        }
    }
}
