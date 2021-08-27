using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class GenreService : IGenreService
    {
        private readonly IAsyncRepository<Genre> _genreRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly IMovieService _movieService;
        // Caching...
        public GenreService(IAsyncRepository<Genre> genreRepository, IMemoryCache memoryCache, IMovieService movieService)
        {
            _genreRepository = genreRepository;
            _memoryCache = memoryCache;
            _movieService = movieService;
        }

        public async Task<IEnumerable<GenreResponseModel>> GetAllGenres()
        {
            // check if the cache has the genres, if yes then take genres from cache
            // If no, then go to database and get the genres and store in cache
            // .NET in memory caching => small amount of data
            // Distributed caching -> Redis Cache => large amount of data
            var genres = await _memoryCache.GetOrCreateAsync("genresData", async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromDays(30);
                return await _genreRepository.ListAllAsync();
            });
            //var genres = await _genreRepository.ListAllAsync();
            var genresModel = new List<GenreResponseModel>();

            foreach (var genre in genres)
            {
                genresModel.Add(new GenreResponseModel 
                {
                    Id = genre.Id,
                    Name = genre.Name 
                });
            }
            return genresModel;
        }

        public async Task<GenreResponseModel> GetGenreDetails(int id)
        {
            var topMovies = await _movieService.GetTopRevenueMovies();
            var genreMovies = new GenreResponseModel();
            var genreDetails = await _genreRepository.GetByIdAsync(id);
            genreMovies.movies = new List<MovieCardResponseModel>();
            foreach (var movie in topMovies)
            {
                var movieDetails = await _movieService.GetMovieDetails(movie.Id);
                foreach (var genre in movieDetails.Genres)
                {
                    if (genre.Id == id)
                    {
                        genreMovies.Id = id;
                        genreMovies.Name = genreDetails.Name;
                        genreMovies.movies.Add(new MovieCardResponseModel
                        {
                            Id = movie.Id,
                            Title = movie.Title,
                            PosterUrl = movie.PosterUrl
                        });
                    }
                }
            }
            return genreMovies;
        }
    }
}
