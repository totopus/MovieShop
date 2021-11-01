using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using ApplicationCore.Entities;
using System.Diagnostics;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IReviewRepository _reviewRepository;

        public UserService(IUserRepository userRepository, IPurchaseRepository purchaseRepository, IFavoriteRepository favoriteRepository, IReviewRepository reviewRepository)
        {
            _userRepository = userRepository;
            _purchaseRepository = purchaseRepository;
            _favoriteRepository = favoriteRepository;
            _reviewRepository = reviewRepository;
        }



        //=============================== User Register ==================================//
        public async Task<int> RegisterUser(UserRegisterRequestModel requestModel)
        {
            // check whether email exists in the database
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
            if (dbUser != null)
                //email exists in the database
                throw new Exception("Email already exists, please login");

            // generate a random unique salt
            var salt = GetSalt();

            // create the hashed password with salt generated in the above step
            var hashedPassword = GetHashedPassword(requestModel.Password, salt);

            // save the user object to db
            // create user entity object
            var user = new User
            {
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                Email = requestModel.Email,
                Salt = salt,
                HashedPassword = hashedPassword,
                DateOfBirth = requestModel.DateOfBirth
            };

            // use EF to save this user in the user table
            var newUser = await _userRepository.Add(user);
            return newUser.Id;
        }
        private string GetSalt()
        {
            var randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }
        private string GetHashedPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                Convert.FromBase64String(salt),
                KeyDerivationPrf.HMACSHA512,
                10000,
                256 / 8));
            return hashed;
        }
        //=======================================================================================//

        //======================================== User Login ===================================//
        public async Task<UserLoginResponseModel> LoginUser(UserLoginRequestModel requestModel)
        {
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
            if (dbUser == null)
            {
                throw null;
            }
            var hashedPassword = GetHashedPassword(requestModel.Password, dbUser.Salt);
            //check hashedpassword with database hashed password
            if (hashedPassword == dbUser.HashedPassword)
            {
                var userLoginResponseModel = new UserLoginResponseModel
                {
                    Id = dbUser.Id,
                    FirstName = dbUser.FirstName,
                    LastName = dbUser.LastName,
                    DateOfBirth = dbUser.DateOfBirth.GetValueOrDefault(),
                    Email = dbUser.Email
                };
                return userLoginResponseModel;
            }
            return null;
        }

        //=======================================================================================//

        //=================================== Get User Purchased Movies ===============================//
        public async Task<List<PurchaseDetailsResponseModel>> GetPurchasedMovieByUserId(int id)
        {
            var purchase = await _purchaseRepository.GetPurchasedByUserId(id);
            if (purchase == null)
            {
                throw new Exception($"No User Found for this {id}");
            }

            var purchaseDetails = new List<PurchaseDetailsResponseModel>();

            foreach (var movie in purchase)
            {
                //Debug.WriteLine($"{movie.PurchaseDateTime}");
                purchaseDetails.Add(new PurchaseDetailsResponseModel
                {
                    Id = movie.MovieId,
                    Title = movie.Movie.Title,
                    PosterUrl = movie.Movie.PosterUrl,
                    PurchaseDateTime = movie.PurchaseDateTime,
                    TotalPrice = movie.TotalPrice,
                    PurchaseNumber = movie.PurchaseNumber
                });
            }
            return purchaseDetails;
        }
        //==============================================================================================//
        //======================================== Purchase Movie ======================================//
        public async Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
            var movieId = purchaseRequest.MovieId;
            var movie = await _purchaseRepository.GetPurchaseDetails(userId, movieId);

            var purchase = new Purchase
            {
                UserId = userId,
                PurchaseNumber = purchaseRequest.PurchaseNumber,
                TotalPrice = purchaseRequest.Price,
                PurchaseDateTime = purchaseRequest.PurchaseDateTime,
                MovieId = purchaseRequest.MovieId
            };

            //Debug.WriteLine($"userid: {purchase.UserId}\n purchasenumber: {purchase.PurchaseNumber}\n totalprice: {purchase.TotalPrice}\n purchasedatetime: {purchase.PurchaseDateTime}\n movieid: {purchase.MovieId}");


            var newPurchase = await _purchaseRepository.Add(purchase);
            return true;
        }
        //=====================================================================================================//
        //===================================== Check If Movie Is Purchased ===================================//
        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        {

            var isPurchased = await _purchaseRepository.GetExistsAsync(m => m.MovieId == purchaseRequest.MovieId && m.UserId == userId);
            return isPurchased;
        }
        //==================================================================================//

        //=============================== Favorite Movie ====================================//
        public async Task<bool> AddFavorite(FavoriteRequestModel favoriteRequest, int userId)
        {

            //var movie = await _purchaseRepository.GetPurchaseDetails(userId, movieId);
            var favorite = new Favorite
            {
                UserId = userId,
                MovieId = favoriteRequest.MovieId
            };
            //Debug.WriteLine($"userid: {userId}\n movieid: {favorite.MovieId}");
            var newFavorite = await _favoriteRepository.Add(favorite);
            return true;
        }
        //===================================================================================//

        //============================ Get User Favorite Movies =============================//
        public async Task<List<FavoriteResponseModel>> GetAllFavoritesForUser(int userId)
        {
            var movies = await _favoriteRepository.GetUserFavoriteMovies(userId);
            var favorites = new List<FavoriteResponseModel>();
            foreach (var movie in movies)
            {
                //Debug.WriteLine($"{movie.Title}");
                favorites.Add(new FavoriteResponseModel
                {

                    UserId = userId,
                    MovieId = movie.MovieId,
                    PosterUrl = movie.Movie.PosterUrl,
                    Title = movie.Movie.Title
                });
            }

            return favorites;
        }
        //===========================================================================//
        //================================= Add Movie Review===================================//
        public async Task<bool> AddMovieReview(ReviewRequestModel reviewRequest,int userId)
        {
            var review = new Review
            {
                MovieId = reviewRequest.MovieId,
                UserId = userId,
                Rating = reviewRequest.Rating,
                ReviewText = reviewRequest.ReviewText
            };
            //Debug.WriteLine($"userid: {userId}\n movieid: {favorite.MovieId}");
            var newReview = await _reviewRepository.Add(review);
            return true;
        }
        //===========================================================================================//

        //======================================= Is Movie Favorited ========================================//
        public async Task<bool> IsMovieFavorited(FavoriteRequestModel favoriteRequest, int userId)
        {
            var isFavorited = await _favoriteRepository.GetExistsAsync(m => m.MovieId == favoriteRequest.MovieId && m.UserId == userId);
            return isFavorited;
        }

        //======================================================================================================//
        //=========================================== Remove Favorite Movie =========================================//
        public async Task<bool> RemoveFavorite(FavoriteRequestModel favoriteRequest,int userId)
        {
            var favorites = await _favoriteRepository.Get(f => f.MovieId == favoriteRequest.MovieId && f.UserId == userId);
            var result = await _favoriteRepository.Delete(favorites.FirstOrDefault());
            return result > 0;
        }

        //=====================================================================================//

        //================================ Get All Reviews By User ==================================//
        public async Task<IEnumerable<UserReviewResponseModel>> GetAllReviewsByUser(int userId)
        {
            var reviews = await _reviewRepository.GetReviewsByUser(userId);
            if (reviews == null) throw new Exception("You haven't reviewed any movies");
            var result = reviews.Select(r => new UserReviewResponseModel
            {
                UserId = r.UserId,
                MovieId = r.MovieId,
                Title = r.Movie.Title,
                PosterUrl = r.Movie.PosterUrl,
                ReviewText = r.ReviewText,
                Rating = r.Rating,
                FirstName = r.User.FirstName,
                LastName = r.User.LastName
                
            });
            return result;
        }
        //=================================================================================//

        //=============================== Delete Review ===================================//
        public async Task<bool> DeleteMovieReview(int userId, int movieId)
        {
            var review = await _reviewRepository.Get(f => f.MovieId == movieId && f.UserId == userId);
            var result = await _reviewRepository.Delete(review.FirstOrDefault());
            return result > 0;
        }
        //======================================================================================//

        //=================================== Update Movie Review =======================================//

        public async Task<bool> UpdateMovieReview(ReviewRequestModel reviewRequest, int userId)
        {
            var review = new Review
            {
                UserId = userId,
                MovieId = reviewRequest.MovieId,
                Rating = reviewRequest.Rating,
                ReviewText = reviewRequest.ReviewText
            };

            var result = await _reviewRepository.Update(review);
            return result.UserId>0;
        }

        

        

        
    }
}