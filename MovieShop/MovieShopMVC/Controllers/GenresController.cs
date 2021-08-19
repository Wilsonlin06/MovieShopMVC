using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    public class GenresController : Controller
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        public async Task<IActionResult> Index()
        {
            // Show List of Genres in the header of LayoutPage Hint: Use PartialView and use Bootstrap dropdown to show genres
            // Use <a> with name of genre and when clicked go to database and show list of movies belong to that genre
            var genres = await _genreService.GetAllGenres();
            return View(genres);
        }
    }
}
