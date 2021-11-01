using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Services;
using System.Diagnostics;

namespace MovieShopMVC.Controllers
{
    // all the action methods in User Controller should work only when user is Authenticated (login success)
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;


        public UsersController(IUserService userService, ICurrentUserService currentUserService)
        {
            _userService = userService;
            _currentUserService = currentUserService;
        
        }

        [HttpPost]
        public async Task<IActionResult> Purchase(PurchaseRequestModel requestModel)
        {
            //Debug.WriteLine(requestModel.MovieId);
            var userId = _currentUserService.UserId;  
            var newPurchase = await _userService.PurchaseMovie(requestModel,userId);
            return RedirectToAction("Purchases");
        }

        [HttpPost]
        public async Task<IActionResult> Favorite(FavoriteRequestModel requestModel)
        {
            var userId = _currentUserService.UserId;
            var newFavorite = await _userService.AddFavorite(requestModel,userId);
            return RedirectToAction("Favorites");
        }

        [HttpPost]
        public async Task<IActionResult> Unfavorite(FavoriteRequestModel requestModel)
        {

            var userId = _currentUserService.UserId;
            await _userService.RemoveFavorite(requestModel, userId);

            return Redirect($"~/Movies/Details/{requestModel.MovieId}");
            
        }
        [HttpPost]
        public async Task<IActionResult> DeleteReview(ReviewRequestModel requestModel)
        {
            var userId = _currentUserService.UserId;
            await _userService.DeleteMovieReview(userId,requestModel.MovieId);
            return Redirect($"~/Movies/Details/{requestModel.MovieId}");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateReview(ReviewRequestModel requestModel)
        {
            var userId = _currentUserService.UserId;
            await _userService.UpdateMovieReview(requestModel,userId);
            return RedirectToAction("Reviews");
        }

        [HttpPost]
        public async Task<IActionResult> Review(ReviewRequestModel requestModel)
        {
            var userId = _currentUserService.UserId;
            var newReview = await _userService.AddMovieReview(requestModel, userId);
            return RedirectToAction("Reviews");
        }

        
        
        [HttpGet]
        //Filters in Asp.Net
        [Authorize]
        public async Task<IActionResult> Purchases()
        {
            var userId = _currentUserService.UserId;
            var moviePurchased = await _userService.GetPurchasedMovieByUserId(userId);
            return View(moviePurchased);
        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            var userId = _currentUserService.UserId;
            var movieFavorited = await _userService.GetAllFavoritesForUser(userId);

            return View(movieFavorited);
        }

        [HttpGet]
        public async Task<IActionResult> Reviews()
        {
            // get all the reviews done by this user
            var userId = _currentUserService.UserId;
            var reviews = await _userService.GetAllReviewsByUser(userId);
            return View(reviews);
        }
    }
}