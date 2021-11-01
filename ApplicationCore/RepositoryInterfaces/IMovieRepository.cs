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
        Task<IEnumerable<Movie>> GetTop25RatedMovies();
        Task<Movie> GetMovieById(int id);
        Task<double> GetAverageRatingById(int id);
        Task<IEnumerable<MovieGenre>> GetMoviesByGenreId(int id);
        Task<IEnumerable<Review>> GetMovieReviews(int id, int pageSize = 30, int page = 1);
    }
}
