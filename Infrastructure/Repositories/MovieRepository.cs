using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        public MovieShopDbContext _dbContext;
        public MovieRepository(MovieShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Movie> GetTop30RevenueMovies()
        {
            // we are gonna use EF with LINQ to get top 30 movies by revenue
            // SQL select top 30 * from Movies order by Revenue

            var movies = _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToList();
            return movies;
        }
    }

    
       
    


}
