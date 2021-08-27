using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IMovieRepository : IAsyncRepository<Movie>
    {
        Task<IEnumerable<Movie>> Get30HighestRevenueMovies();
        Task<IEnumerable<Movie>> GetMovies();
        Task<IEnumerable<Movie>> Get30HighestRatedMovies();
        Task<IEnumerable<Genre>> GetMoviesByGenreId(int genreId);
        Task<List<Review>> GetReviewByMovieId(int id);
    }
}
