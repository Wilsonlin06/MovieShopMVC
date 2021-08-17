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
        public UserController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        // GET: /<controller>/
        // user/GetAllPurchases
        //[Authorize]
        public async Task<IActionResult> GetAllPurchases()
        {
            var userId = _currentUserService.UserId;
            // id from the cookie and send that id to UserService to get all his/her movies.
            // Filters **Important interview question** 
            return View();
        }

        //[Authorize]
        public async Task<IActionResult> GetFavorites()
        {
            return View();
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
        public async Task<IActionResult> FavoriteMovie()
        {
            return View();
        }
    }
}
