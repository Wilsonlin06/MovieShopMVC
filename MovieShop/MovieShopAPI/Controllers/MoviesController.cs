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

        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _movieService.GetAllMovies();
            if(!movies.Any())
            {
                return NotFound("No movies found");
            }
            return Ok(movies);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);
            if(movie == null)
            {
                return NotFound("No movie was found with the id.");
            }
            return Ok(movie);
        }

        [HttpGet]
        [Route("TopRated")]
        public async Task<IActionResult> GetTopRatedMovies()
        {
            var movies = await _movieService.GetTopRatedMovies();
            if (!movies.Any())
            {
                return NotFound("No Movies Found");
            }
            return Ok(movies);
        }

        // api/movies/toprevenue
        [HttpGet]
        [Route("TopRevenue")]
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            var movies = await _movieService.GetTopRevenueMovies();
            if (!movies.Any())
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

        [HttpGet]
        [Route("Genre/{genreId:int}")]
        public async Task<IActionResult> GetMovieByGenreId(int genreId)
        {
            var genre = await _movieService.GetMovieByGenreId(genreId);
            if(genre == null)
            {
                return NotFound("Genre was not found with the id");
            }
            return Ok(genre);
        }

        [HttpGet]
        [Route("{movieId:int}/Reviews")]
        public async Task<IActionResult> GetReviewByMovieId(int movieId)
        {
            var reviews = await _movieService.GetReviewByMovieId(movieId);
            if(!reviews.Any())
            {
                return NotFound("Review not found with the MovieId");
            }
            return Ok(reviews);
        }
    }
}
