using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FavoriteRepository : EfRepository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
                
        }
        public async Task<Favorite> GetFavoriteMovieDetails(FavoriteRequestModel model)
        {
            return await _dbContext.Favorites.Include(f => f.Movie).Where(f => f.MovieId == model.MovieId && f.UserId == model.UserId)
                .FirstOrDefaultAsync();
            
        }
    }
}
