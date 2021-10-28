using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CastRepository : ICastRepository
    {
        public MovieShopDbContext _dbContext;
        public CastRepository(MovieShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Cast> GetCastById(int id)
        {
            var cast = await _dbContext.Casts.Include(m=>m.Movies).FirstOrDefaultAsync(c => c.Id==id);
            return cast;
        }
    }
}
