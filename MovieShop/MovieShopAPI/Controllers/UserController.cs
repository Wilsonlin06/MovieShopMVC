using ApplicationCore.Models;
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
    //[Authorize]
    public class UserController : ControllerBase
    {
        //private readonly ICurrentUserService _currentUserService;
        private readonly IUserService _userService;
        public UserController(ICurrentUserService currentUserService, IUserService userService)
        {
            //_currentUserService = currentUserService;
            _userService = userService;
        }

        [HttpPost]
        [Route("Purchases")]
        public async Task<IActionResult> GetAllPurchases([FromBody] int userId)
        {
            //var userId = _currentUserService.UserId;
            // id from the cookie and send that id to UserService to get all his/her movies.
            // Filters **Important interview question** 
            var movieCards = await _userService.GetPurchasedMovies(userId);
            return Ok(movieCards);
        }

        [HttpPost]
        [Route("Favorites")]
        public async Task<IActionResult> Favorites([FromBody] int userId)
        {
            //var userId = _currentUserService.UserId;
            var movieCards = await _userService.GetFavorites(userId);
            return Ok(movieCards);
        }

    }
}
