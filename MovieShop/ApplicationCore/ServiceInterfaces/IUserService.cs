using ApplicationCore.Entities;
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
        Task<UserRegisterResponseModel>  RegisterUser(UserRegisterRequestModel model);
        Task<UserLoginResponseModel>  Login(LoginRequestModel model);

        //Task<IEnumerable<PurhchaseRequestModel>> AddPurchasedMovies(PurhchaseRequestModel model);
        Task<IEnumerable<MovieCardResponseModel>> GetPurchasedMovies(int userId);
        Task<IEnumerable<MovieCardResponseModel>> GetAllFavorites(int userId);
        Task<MovieDetailsResponseModel> GetFavorite(int userId, int movieId);
        Task<UserRegisterResponseModel> GetUserInfo(int userId);
        Task<List<UserRegisterRequestModel>> GetAllUsers();

        Task<FavoriteRequestModel> SetFavorite(FavoriteRequestModel model);
        Task<FavoriteRequestModel> UnFavorite(FavoriteRequestModel model);
        Task<ReviewRequestModel> AddReview(ReviewRequestModel model);
        Task<Review> UpdateReview(ReviewRequestModel model);
    }
}
