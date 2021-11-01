using ApplicationCore.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore;
using ApplicationCore.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories
{
    public class FavoriteRepository : EfRepository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Favorite>> GetUserFavoriteMovies(int id)
        {
            var movieFavorited = await _dbContext.Favorites.Include(m => m.Movie).Where(u => u.UserId == id).ToListAsync();
            return movieFavorited;
        }

      
    }
}
