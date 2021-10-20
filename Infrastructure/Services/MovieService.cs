using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MovieService
    {
        public List<MovieCardResponseModel> GetTop30RevenueMovies()
        {
            //method should call movie repo and get data from movie table
            
            var movieCards = new List<MovieCardResponseModel> 
            { 
                new MovieCardResponseModel {Id = 1, Title="Inception", PosterUrl = "https://image.tmdb.org/t/p/w342//9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg" },
                new MovieCardResponseModel {Id = 3, Title = "The Dark Knight" , PosterUrl="https://image.tmdb.org/t/p/w342//qJ2tW6WMUDux911r6m7haRef0WH.jpg"},
                new MovieCardResponseModel { Id = 2, Title="Interstellar", PosterUrl="https://image.tmdb.org/t/p/w342//gEU2QniE6E77NI6lCU6MxlNBvIx.jpg"}
            };

            return movieCards;
        }
    }
}
