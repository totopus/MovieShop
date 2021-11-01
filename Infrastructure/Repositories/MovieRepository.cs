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
    public class MovieRepository :EfRepository<Movie>, IMovieRepository
    {
        //public MovieShopDbContext _dbContext;
        public MovieRepository(MovieShopDbContext dbContext):base(dbContext)
        {
            //_dbContext = dbContext;
        }

        //============================== Get Average Rating By ID ======================================//
        public async Task<double> GetAverageRatingById(int id)
        {
            var averageRating = await _dbContext.Reviews.Where(r => r.MovieId == id).DefaultIfEmpty().AverageAsync(r => r == null ? 0: r.Rating);
            return (double) averageRating;
        }   


        //=============================== Get Movie By ID ======================================//
        public async Task<Movie> GetMovieById(int id)
        {
            var movie = await _dbContext.Movies.Include(m => m.Casts).ThenInclude(m => m.Cast)
                .Include(m => m.Genres).ThenInclude(m => m.Genre).Include(m => m.Trailers)
                .FirstOrDefaultAsync(m => m.Id == id);
            return movie;

            // First vs FirstOrDefault
            // Single (should be only 1 0, more than 1 exception) vs SingleOrDefault (0,1, more than 1 exception)
        }

        //=================================== Get Top 30 Revenue Movies ===========================================//
        public async Task<IEnumerable<Movie>> GetTop30RevenueMovies()
        {
            // we are gonna use EF with LINQ to get top 30 movies by revenue
            // SQL select top 30 * from Movies order by Revenue
            // I/O bound operation
            // you can await only Tasks
            // EF and dapper have both sync and async methods
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;
        }
    }

    
       
    


}
