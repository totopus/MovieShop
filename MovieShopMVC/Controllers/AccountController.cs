using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{

    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;

        }
        //post when submit
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel requestModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var newUser = await _userService.RegisterUser(requestModel);
            return RedirectToAction("Login");
        }

        //get empty view
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequestModel requestModel)
        {
            var user = await _userService.LoginUser(requestModel);
            if (user == null)
            {
                return View();
            }
            //create cookie and store information in the cookie and cookie will have expiration time
            //tell the asp.net application that we use cookie based authentication and specify the
            //details of cookie like name, how long the cookie is valid where to redirect when cookie expired

            //Claims=>
            //Driver license=>Name,DateofBirth,Expire
            //create all necessary claims inside claims object
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.LastName),
                new Claim(ClaimTypes.Surname,user.FirstName),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.DateOfBirth,user.DateOfBirth.ToShortDateString()),

                new Claim("FullName",user.FirstName+" " +user.LastName)
            };

            //identity
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            //prit out card
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
            
            return LocalRedirect("~/");
            //logout=>
        }

        public async Task<IActionResult> Logout()
        {
            //invalidate cookie and redirect to login
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
