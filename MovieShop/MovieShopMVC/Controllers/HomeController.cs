using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Clean Architecture

        // Interfaces

        // IMovieServices

        // class MovieService : IMovieService, IGenreService
        // {
        // }

        // class MovieService2 : IMovieSrvice
        // {
        // }

        // class MovieService3 : SomeClass, IMovieSrvice
        // {
        // }

        // Dependency Injection
        // Application Core Layer
        // Entities => C# classes that represents your domain/database objects
        // Models =>

        // 15 tables
        // 20 columns, Movie Table
        // Entity => Movie 20 properties

        // Models => UI
        // Movie list => MovieCardModel => id, title, posterimage
        // DTO (Data Transfer Objects) => API
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
