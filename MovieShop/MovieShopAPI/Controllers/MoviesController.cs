using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieShopAPI.Controllers
{
    // Attribute Routing
    [Route("api/[controller]")]
    //[ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        // api/movies/toprevenue
        [Route("toprevenue")]
        [HttpGet]
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            var movies = await _movieService.GetTopRevenueMovies();
            if(!movies.Any())
            {
                return NotFound("No Movies Found");
            }
            // 200 OK
            return Ok(movies);
            // Serialization => object to another type of object
            // C# to JASON
            // DeSerialization => JSON to C#
            // .NET Core 3.1 or less, JSON.NET => 3rd party library, included
            // System.Text.Json =>
            // along with data you also need to return HTTP status code
        }
    }
}
