using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IMovieService
    {
        Task<List<MovieCardResponseModel>> GetTop30RevenueMovies();
        Task<List<MovieCardResponseModel>> GetTop25RatedMovies();
        Task<List<MovieCardResponseModel>> GetAllMovies();
        Task<MovieDetailsResponseModel> GetMovieDetails(int movieId, int? userId=null);
        Task<List<MovieCardResponseModel>> GetMoviesByGenreId(int id);
        Task<double> GetAverageRatingById(int id);
        Task<List<UserReviewResponseModel>> GetReviewsByMovieId(int id);

    }
}
