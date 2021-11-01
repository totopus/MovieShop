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
        Task<MovieDetailsResponseModel> GetMovieDetails(int movieId, int? userId);

        Task<double> GetAverageRatingById(int id);
    }
}
