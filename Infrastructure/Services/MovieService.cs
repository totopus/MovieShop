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

        //==================================== Get All Movies =====================================//
        public async Task<List<MovieCardResponseModel>> GetAllMovies()
        {
            var movies = await _movieRepository.GetAll();
            var movieList = new List<MovieCardResponseModel>();

            foreach (var movie in movies)
            {
                movieList.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    PosterUrl = movie.PosterUrl
             
                });
            }
            return movieList;
           
        }

        //========================================= Get Average Rating By ID =====================================//
        public async Task<double> GetAverageRatingById(int id)
        {
            var averageRating = await _movieRepository.GetAverageRatingById(id);
            return averageRating;
        }


        //============================= Get Movie Details =================================//
        public async Task<MovieDetailsResponseModel> GetMovieDetails(int movieId, int? userId=null)
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

        //============================== Get Movies By GenreId ================================//
        public async Task<List<MovieCardResponseModel>> GetMoviesByGenreId(int id)
        {
            var movies = await _movieRepository.GetMoviesByGenreId(id);
            var result = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                result.Add(
                    new MovieCardResponseModel()
                    {
                        Id = movie.MovieId,
                        Title = movie.Movie.Title,
                        PosterUrl = movie.Movie.PosterUrl

                    });
            }
            return result;
        }

        //================================ Get Reviews By MovieId ==============================//
        public async Task<List<UserReviewResponseModel>> GetReviewsByMovieId(int id)
        {
            var reviews = await _movieRepository.GetMovieReviews(id);
            var reviewsList = new List<UserReviewResponseModel>();
            foreach (var review in reviews)
            {
                reviewsList.Add(new UserReviewResponseModel
                {
                    UserId = review.UserId,
                    ReviewText = review.ReviewText,
                    Rating = review.Rating,
                    FirstName = review.User.FirstName,
                    LastName = review.User.LastName
                });
            }
            return reviewsList;
        }

        //==================================== Get Top 25 Rated Movies ================================//
        public async Task<List<MovieCardResponseModel>> GetTop25RatedMovies()
        {
            var movies = await _movieRepository.GetTop25RatedMovies();
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
