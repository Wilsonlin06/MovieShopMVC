using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext): base(dbContext)
        {

        }

        public async Task<IEnumerable<Movie>> Get30HighestRatedMovies()
        {
            var movies = await _dbContext.Reviews.Include(m => m.Movie)
                                                 .GroupBy(r => new { id = r.MovieId, r.Movie.PosterUrl, r.Movie.Title, r.Movie.ReleaseDate })
                                                 .OrderByDescending(g => g.Average(m => m.Rating))
                                                 .Select(m => new Movie
                                                 {
                                                     Id = m.Key.id,
                                                     PosterUrl = m.Key.PosterUrl,
                                                     Title = m.Key.Title,
                                                     ReleaseDate = m.Key.ReleaseDate

                                                 }).Take(30).ToListAsync();

            return movies;
        }

        public async Task<IEnumerable<Movie>> Get30HighestRevenueMovies()
        {
            // get 30 movies from movie table order by revenue
            // select top 30 from movies order by revenue;
            // ToList(), Count() or we can loop through
            // I/O bound operation
            // EF has methods that have both async and non-async ones
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();

            return movies;
            
        }

        public override async Task<Movie> GetByIdAsync(int Id)
        {
            // movie table, then genres, then casts and rating
            // Include() ThenInclude()

            var movie = await _dbContext.Movies.Include(m => m.MovieCasts).ThenInclude(m => m.Cast).Include(m => m.Genres)
                .FirstOrDefaultAsync(m => m.Id == Id);

            if (movie == null)
            {
                throw new Exception($"No Movie Found for the id {Id}");
            }

            var movieRating = await _dbContext.Reviews.Where(m => m.MovieId == Id).DefaultIfEmpty()
                .AverageAsync(r => r == null ? 0 : r.Rating);

            movie.Rating = movieRating;
            return movie;
        }

        public async Task<IEnumerable<Genre>> GetMoviesByGenreId(int genreId)
        {
            var movies = await _dbContext.Genres.Include(t => t.Movies).Where(g => g.Id == genreId).ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            var movies = await _dbContext.Movies.ToListAsync();
            return movies;
        }

        public async Task<List<Review>> GetReviewByMovieId(int movieId)
        {
            var reviews = await _dbContext.Reviews.Where(r => r.MovieId == movieId).ToListAsync();
            return reviews;
        }
    }
}
