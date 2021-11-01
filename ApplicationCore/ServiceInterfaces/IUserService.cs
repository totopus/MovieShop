using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IUserService
    {
        Task<int> RegisterUser(UserRegisterRequestModel requestModel);
        Task<UserLoginResponseModel> LoginUser(UserLoginRequestModel requestModel);
        Task<List<PurchaseDetailsResponseModel>> GetPurchasedMovieByUserId(int id);
        Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId);
        Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId);
        Task<bool> AddFavorite(FavoriteRequestModel favoriteRequest, int userId);
        Task<bool> AddMovieReview(ReviewRequestModel reviewRequest, int userId);
        Task <List<FavoriteResponseModel>> GetAllFavoritesForUser(int userId);
        Task<bool> IsMovieFavorited(FavoriteRequestModel favoriteRequest, int userId);
        Task<bool> RemoveFavorite(FavoriteRequestModel favoriteRequest, int userId);
        Task<IEnumerable<UserReviewResponseModel>> GetAllReviewsByUser(int id);
        Task<bool> DeleteMovieReview(int userId, int movieId);
        Task<bool> UpdateMovieReview(ReviewRequestModel reviewRequest,int userId);

    }
}
