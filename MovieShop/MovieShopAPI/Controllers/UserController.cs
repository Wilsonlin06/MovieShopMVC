using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly ICurrentUserService _currentUserService;
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("Purchase")]
        public async Task<IActionResult> AddPurchases([FromBody] int userId)
        {
            //var userId = _currentUserService.UserId;
            // id from the cookie and send that id to UserService to get all his/her movies.
            // Filters **Important interview question** 
            var movieCards = await _userService.GetPurchasedMovies(userId);
            return Ok(movieCards);
        }
        [HttpGet]
        [Authorize]
        [Route("{id:int}/Purchases")]
        public async Task<IActionResult> GetAllPurchases(int id)
        {
            var movieCards = await _userService.GetPurchasedMovies(id);
            return Ok(movieCards);
        }

        [HttpGet]
        [Route("{id:int}/movie/{movieId:int}/favorite")]
        public async Task<IActionResult> GetFavorite(int userId, int movieId)
        {
            var favorites = await _userService.GetFavorite(userId, movieId);
            return Ok(favorites);
        }

        [HttpGet]
        //[Authorize]
        [Route("{id:int}/Favorites")]
        public async Task<IActionResult> GetAllFavorites(int id)
        {
            var favorites = await _userService.GetAllFavorites(id);
            return Ok(favorites);
        }

        [HttpPost]
        [Route("Favorites")]
        public async Task<IActionResult> Favorites([FromBody] FavoriteRequestModel model)
        {
            var favorites = await _userService.SetFavorite(model);
            return Ok(favorites);
        }

        [HttpPost]
        [Route("UnFavorites")]
        public async Task<IActionResult> UnFavorites([FromBody] FavoriteRequestModel model)
        {
            var favorites = await _userService.UnFavorite(model);
            return Ok(favorites);
        }

        [HttpPost]
        [Route("Review")]
        public async Task<IActionResult> AddReview([FromBody] ReviewRequestModel model)
        {
            var review = await _userService.AddReview(model);
            return Ok(review);
        }

        [HttpPut]
        [Route("Review")]
        public async Task<IActionResult> UpdateReview([FromBody] ReviewRequestModel model)
        {
            var review = await _userService.UpdateReview(model);
            if(review == null)
            {
                return BadRequest($"No review found with MovieId: {model.movieId}");
            }
            return Ok(review);
        }
    }
}
