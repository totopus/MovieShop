using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models;
using Infrastructure.Services;
using ApplicationCore.ServiceInterfaces;
using MovieShopMVC.Services;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ICurrentUserService _currentUserService;
        public MoviesController(IMovieService movieService, ICurrentUserService currentUserService)
        {
            _movieService = movieService;
            _currentUserService = currentUserService;

        }

        

        // http://localhost/movie/details/1
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var userId = _currentUserService.UserId;
            var moviedetails = await _movieService.GetMovieDetails(id,userId);
            return View(moviedetails);
        }
    }
}
