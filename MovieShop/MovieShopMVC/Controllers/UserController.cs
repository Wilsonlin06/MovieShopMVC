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
        public async Task<IActionResult> GetAllPurchases()
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
            var movieCards = await _userService.GetFavorites(userId);
            return View(movieCards);
        }

        //[Authorize]
        public async Task<IActionResult> GetProfile()
        {
            return View();
        }

        //[Authorize]
        public async Task<IActionResult> EditProfile()
        {
            return View();
        }

        //[Authorize]
        public async Task<IActionResult> BuyMovie()
        {
            return View();
        }

        //[Authorize]
        //public async Task<IActionResult> FavoriteMovie()
        //{
        //    return View();
        //}
    }
}
