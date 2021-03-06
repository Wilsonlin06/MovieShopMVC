using ApplicationCore.Entities;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserService _userService;
        public UserController(ICurrentUserService currentUserService, IUserService userService)
        {
            _currentUserService = currentUserService;
            _userService = userService;
        }
        // GET: /<controller>/
        // user/GetAllPurchases
        //[Authorize]
        public async Task<IActionResult> Purchases()
        {
            var userId = _currentUserService.UserId;
            // id from the cookie and send that id to UserService to get all his/her movies.
            // Filters **Important interview question** 
            var movieCards = await _userService.GetPurchasedMovies(userId);
            return View(movieCards);
        }

        //[Authorize]
        public async Task<IActionResult> Favorites()
        {
            var userId = _currentUserService.UserId;
            var movieCards = await _userService.GetAllFavorites(userId);
            return View(movieCards);
        }

        //[Authorize]
        public async Task<IActionResult> Profile()
        {
            var userId = _currentUserService.UserId;
            // id from the cookie and send that id to UserService to get all his/her movies.
            // Filters **Important interview question** 
            var userProfile = await _userService.GetUserInfo(userId);
            return View(userProfile);
        }

        //[Authorize]
        public Task<IActionResult> EditProfile()
        {
            throw new NotImplementedException();
        }

        //[Authorize]
        public Task<IActionResult> BuyMovie()
        {
            throw new NotImplementedException();
        }

        //[Authorize]
        //public async Task<IActionResult> FavoriteMovie()
        //{
        //    return View();
        //}
    }
}
