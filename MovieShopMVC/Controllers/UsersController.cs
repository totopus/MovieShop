using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    // all the action methods in User Controller should work only when user is Authenticated (login success)
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Purchase()
        {
            // purchase a movie when user clicks on Buy button on Movie Details Page
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Favorite()
        {
            // favorite a movie when user clicks on Favorite Button on Movie Details Page
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Review()
        {
            // add a new review done by the user for that movie
            return View();
        }

        
        
        [HttpGet]
        //Filters in Asp.Net
        //[Authorize]
        public async Task<IActionResult> Purchases(int id)
        {
            var moviePurchased = await _userService.GetPurchasedMovieByUserId(id);
            return View(moviePurchased);
            // get all the movies purchased by user => List<MovieCard> 
            //var userIdentity = this.User.Identity;
            //if (userIdentity != null && userIdentity.IsAuthenticated)
            //{
            //    return View();
            //}
            //RedirectToAction("Login", "Account");

            //get all movies purchased by user =>list<moviecard>
            //int userId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            //call userservice that will give list of moviecard models that this user purchased
            //purchase, dbContext.Purchase.where(u=>u.userid==id);
          
        }

        [HttpGet]
        public async Task<IActionResult> Favorites(int id)
        {
            // get all movies favorited by that user
            return View();
        }

        public async Task<IActionResult> Reviews(int id)
        {
            // get all the reviews done by this user
            return View();
        }
    }
}