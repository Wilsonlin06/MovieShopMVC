using ApplicationCore.Exceptions;
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

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        //private readonly IAsyncRepository<Favorite> _favoriteRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            //_favoriteRepository = favoriteRepository;
        }

        public async Task<List<UserRegisterRequestModel>> GetAllUsers()
        {
            var users = await _userRepository.ListAsync(m => m.Id < 1000);
            var userList = new List<UserRegisterRequestModel>();
            foreach (var user in users)
            {
                userList.Add(new UserRegisterRequestModel
                {
                    DateOfBirth = (DateTime)user.DateOfBirth,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                });
            }
            return userList;
        }

        public async Task<MovieDetailsResponseModel> GetFavorite(int userId, int movieId)
        {
            var dbUser = await _userRepository.GetUserFavoriteById(userId);
            var movieDetails = new MovieDetailsResponseModel { };

            foreach (var fav in dbUser.Favorites)
            {
                if (fav.MovieId == movieId)
                {
                    movieDetails.Id = fav.Movie.Id;
                    movieDetails.Title = fav.Movie.Title;
                    movieDetails.Overview = fav.Movie.Overview;
                    movieDetails.Tagline = fav.Movie.Tagline;
                    movieDetails.Budget = fav.Movie.Budget;
                    movieDetails.Revenue = fav.Movie.Revenue;
                    movieDetails.ImdbUrl = fav.Movie.ImdbUrl;
                    movieDetails.TmdbUrl = fav.Movie.TmdbUrl;
                    movieDetails.PosterUrl = fav.Movie.PosterUrl;
                    movieDetails.BackdropUrl = fav.Movie.BackdropUrl;
                    movieDetails.OriginalLanguage = fav.Movie.OriginalLanguage;
                    movieDetails.ReleaseDate = fav.Movie.ReleaseDate;
                    movieDetails.RunTime = fav.Movie.RunTime;
                    movieDetails.Price = fav.Movie.Price;
                    movieDetails.Rating = fav.Movie.Rating;
                    break;
                }
            }
            return movieDetails;
        }
        public async Task<IEnumerable<MovieCardResponseModel>> GetAllFavorites(int userId)
        {
            var dbUser = await _userRepository.GetUserFavoriteById(userId);
            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in dbUser.Favorites)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.MovieId,
                    Title = movie.Movie.Title,
                    PosterUrl = movie.Movie.PosterUrl
                });
            }
            return movieCards;
        }

        public async Task<IEnumerable<MovieCardResponseModel>> GetPurchasedMovies(int userId)
        {
            var user = await _userRepository.GetUserPurchaseById(userId);
            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in user.Purchases)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.MovieId,
                    Title = movie.Movie.Title,
                    PosterUrl = movie.Movie.PosterUrl
                });
            }
            return movieCards;
        }

        public async Task<UserRegisterResponseModel> GetUserInfo(int userId)
        {
            var user = await _userRepository.GetUserPurchaseById(userId);
            var userProfile = new UserRegisterResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return userProfile;
        }

        public async Task<UserLoginResponseModel> Login(LoginRequestModel model)
        {
            var dbUser = await _userRepository.GetUserByEmail(model.Email);
            if (dbUser == null)
            {
                return null;
            }

            // get the hashed password and salt from database

            var hashedPassword = GetHashedPassword(model.Password, dbUser.Salt);

            if (hashedPassword == dbUser.HashedPassword)
            {
                // success 
                var userLoginResponseModel = new UserLoginResponseModel
                {
                     Id = dbUser.Id,
                     FirstName = dbUser.FirstName,
                     LastName = dbUser.LastName,
                     Email = dbUser.Email
                };

                return userLoginResponseModel;
            }

            return null;
        }

        public async Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel model)
        {
            // make sure with the user entered email does not exists in database.

            var dbUser = await _userRepository.GetUserByEmail(model.Email);

            if (dbUser != null)
            {
                // user already has email
                throw new ConflictException("Email already exists");
            }

            // user does not exists in the database

            // generate a uniqueue salt

            var salt = GenerateSalt();
            var hashedPassword = GetHashedPassword(model.Password, salt);

            // hash password with salt
            // save the salt and hashed password to the database.

            // create User entity object
            var user = new User
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Salt = salt,
                HashedPassword = hashedPassword,
                DateOfBirth = model.DateOfBirth
            };

            var createdUser = await _userRepository.AddAsync(user);

            var userRegisterResponseModel = new UserRegisterResponseModel
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName
            };

            return userRegisterResponseModel;

        }

        public async Task<FavoriteRequestModel> SetFavorite(FavoriteRequestModel model)
        {
            var favorite = new Favorite
            {
                UserId = model.UserId,
                MovieId = model.MovieId
            };
            var favorites = await _userRepository.AddAsync(favorite);
            return model;
        }

        public async Task<FavoriteRequestModel> UnFavorite(FavoriteRequestModel model)
        {
            //var fav = await _favoriteRepository.ListAsync(u => u.UserId == model.UserId && u.MovieId == model.MovieId);
            //await _favoriteRepository.DeleteAsync(fav.First());
            var fav = await _userRepository.GetUserFavoriteById(model.UserId);
            var favModel = new Favorite { };
            foreach (var f in fav.Favorites)
            {
                if (f.MovieId == model.MovieId)
                {
                    favModel.Id = f.Id;
                    favModel.UserId = model.UserId;
                    favModel.MovieId = model.MovieId;
                    favModel.Movie = f.Movie;
                    favModel.User = f.User;
                    break;
                }
            }
            //var favorite = new Favorite
            //{
            //    Id = favModel.Id,
            //    UserId = favModel.UserId,
            //    MovieId = favModel.MovieId
            //};
            var favorites = await _userRepository.Remove(favModel);
            return model;
        }

        public async Task<Review> UpdateReview(ReviewRequestModel model)
        {
            var existReview = await _userRepository.getReviewByIdAsync(model.movieId, model.userId);
            if (existReview == null)
            {
                return null;
            }
            var review = new Review
            {
                UserId = model.userId,
                MovieId = model.movieId,
                ReviewText = model.reviewText,
                Rating = model.Rating
            };
            await _userRepository.UpdateAsync(review);
            return review;
        }

        public async Task<ReviewRequestModel> AddReview(ReviewRequestModel model)
        {
            var review = new Review
            {
                MovieId = model.movieId,
                UserId = model.userId,
                Rating = model.Rating,
                ReviewText = model.reviewText
            };
            await _userRepository.AddAsync(review);
            return model;
        }

        private string GenerateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }

        private string GetHashedPassword(string password, string salt)
        {
            // Never ever create your own HAshing Algorithms
            // always use Industry tried and tested HAshing Algorithms
            // Aarogon2 
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                                                     password: password,
                                                                     salt: Convert.FromBase64String(salt),
                                                                     prf: KeyDerivationPrf.HMACSHA512,
                                                                     iterationCount: 10000,
                                                                     numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
