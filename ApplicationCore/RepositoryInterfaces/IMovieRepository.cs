using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IMovieRepository: IAsyncRepository<Movie>
    {
        //method that get 30 highest revenue movies
        Task<IEnumerable<Movie>> GetTop30RevenueMovies();
        Task<Movie> GetMovieById(int id);
       // Task<IEnumerable<Review>> GetMovieReviews(int id, int pageSize = 30, int page = 1);
        Task<double> GetAverageRatingById(int id);
    }
}
