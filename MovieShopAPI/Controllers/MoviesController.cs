using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MoviesController : ControllerBase
    {
        //create an api method that shows top 30 revenue movies
        //so that SPA, ios and android app show those movies in 
        //the homescreen
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [Route("")]
        //http://localhost/api/movies
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _movieService.GetAllMovies();
            if (!movies.Any())
            {
                return NotFound("No Movies Found");
            }
            return Ok(movies);
        }

        // create the api method
        //http://localhost/api/movies/3
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);
            if (movie == null)
            {
                return NotFound($"No Movie Found For {id}");
            }
            return Ok(movie);
        }

        [HttpGet]
        [Route("toprating")]
        //http://localhost/api/movies/toprating
        public async Task<IActionResult> GetTopRatingMovies()
        {
            var movies = await _movieService.GetTop25RatedMovies();
            if (!movies.Any())
            {
                return NotFound("No Movies Found");
            }
            return Ok(movies);
        }

        [HttpGet]
        [Route("toprevenue")]
        //http://localhost/api/movies/toprevenue
        //Attribute based routing
        public async Task<IActionResult> GetTopRevenueMovies() 
        {
            var movies = await _movieService.GetTop30RevenueMovies();
            //return Json data and Http Status Code //

            if (!movies.Any())
            {
                //return 404
                return NotFound("No Movies Found");
            }
            //200 ok
            return Ok(movies);

            //for coverting c# objects to json objects there are 2 ways
            //1.before .NET core 3, we used newtonSoft.Json library
            //2.microsoft created their own json serialization library
            //system.text.json
        }

        [HttpGet]
        [Route("genre/{genreId:int}")]
        public async Task<IActionResult> GetMoviesByGenre(int genreId)
        {
            var movies = await _movieService.GetMoviesByGenreId(genreId);
            return Ok(movies);
        }

        [HttpGet]
        [Route("{movieId:int}/reviews")]
        public async Task<IActionResult> GetMovieReviews(int movieId)
        {
            var reviews = await _movieService.GetReviewsByMovieId(movieId);
            if (!reviews.Any())
            {
                return NotFound($"No Reviews For This Movie {movieId}");
            }
            return Ok(reviews);
        }
    }
}
