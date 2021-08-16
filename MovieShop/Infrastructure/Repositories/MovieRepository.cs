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

    }
}
