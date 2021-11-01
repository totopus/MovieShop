using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{

    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IReviewRepository _reviewRepository;
        public MovieService(IMovieRepository movieRepository, IPurchaseRepository purchaseRepository, IFavoriteRepository favoriteRepository,IReviewRepository reviewRepository)
        {
            _movieRepository = movieRepository;
            _purchaseRepository = purchaseRepository;
            _favoriteRepository = favoriteRepository;
            _reviewRepository = reviewRepository;
        }

        public async Task<double> GetAverageRatingById(int id)
        {
            var averageRating = await _movieRepository.GetAverageRatingById(id);
            return averageRating;
        }


        //============================= Get Movie Details =================================//
        public async Task<MovieDetailsResponseModel> GetMovieDetails(int movieId, int? userId)
        {

            var movie = await _movieRepository.GetMovieById(movieId);
            var rating = await GetAverageRatingById(movieId);
            
            
            if (movie == null)
            {
                throw new Exception($"No Movie Found for this {movieId}");
            }
            var movieDetails = new MovieDetailsResponseModel()
            {
                Id = movie.Id,
                Budget = movie.Budget,
                Overview = movie.Overview,
                Price = movie.Price,
                PosterUrl = movie.PosterUrl,
                Revenue = movie.Revenue,
                ReleaseDate = movie.ReleaseDate.GetValueOrDefault(),
                Rating = rating,
                Tagline = movie.Tagline,
                Title = movie.Title,
                RunTime = movie.RunTime,
                BackdropUrl = movie.BackdropUrl,
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl,
                
              
            };

            if (userId != null)
            {
                movieDetails.IsPurchased = await _purchaseRepository.GetExistsAsync(f => f.UserId == userId && f.MovieId == movieId);
                movieDetails.IsFavorited = await _favoriteRepository.GetExistsAsync(f => f.UserId == userId && f.MovieId == movieId);
                movieDetails.IsReviewed = await _reviewRepository.GetExistsAsync(f => f.UserId == userId && f.MovieId == movieId);

            }

            foreach (var genre in movie.Genres)
            {
                movieDetails.Genres.Add(new GenreModel { 
                    Id = genre.GenreId, 
                    Name = genre.Genre.Name });

            }
            
            foreach (var cast in movie.Casts)
            {
                movieDetails.Casts.Add(new CastResponseModel
                {
                    Id = cast.CastId,
                    Character = cast.Character,
                    Name = cast.Cast.Name,
                    ProfilePath = cast.Cast.ProfilePath
                });
            }

            foreach (var trailer in movie.Trailers)
            {
                movieDetails.Trailers.Add(new TrailerResponseModel
                {
                    Id = trailer.Id,
                    Name = trailer.Name,
                    TrailerUrl = trailer.TrailerUrl
                   
                });
            }
            return movieDetails;
            
        }


        //================================= Get Top 30 Revenue Movies ============================//
        public async Task<List<MovieCardResponseModel>> GetTop30RevenueMovies()
        {
            //method should call movie repo and get data from movie table
            var movies = await _movieRepository.GetTop30RevenueMovies();
            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    Title = movie.Title
                });
            }

            return movieCards;
        }

    }
}
