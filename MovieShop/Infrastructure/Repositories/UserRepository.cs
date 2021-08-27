using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {
        public UserRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Favorite> AddAsync(Favorite favorite)
        {
            await _dbContext.AddAsync<Favorite>(favorite);
            var entity = await _dbContext.SaveChangesAsync();
            return favorite;
        }

        public async Task AddAsync(Review review)
        {
            await _dbContext.AddAsync<Review>(review);
            var entity = await _dbContext.SaveChangesAsync();
        }

        public async Task<Review> getReviewByIdAsync(int movieId, int userId)
        {
            var reviewDetails = await _dbContext.FindAsync<Review>(movieId, userId);
            return reviewDetails;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<User> GetUserFavoriteById(int id)
        {
            var fav = await _dbContext.Users.Include(u => u.Favorites).ThenInclude(u => u.Movie)
                .FirstOrDefaultAsync(u => u.Id == id);
            return fav;
        }

        public async Task<User> GetUserPurchaseById(int id)
        {
            var pur = await _dbContext.Users.Include(u => u.Purchases).ThenInclude(u => u.Movie)
                .FirstOrDefaultAsync(u => u.Id == id);
            return pur;
        }

        public async Task<Favorite> Remove(Favorite favorite)
        {
            _dbContext.Set<Favorite>().Remove(favorite);
            var entity = await _dbContext.SaveChangesAsync();
            return favorite;
        }

        public async Task UpdateAsync(Review review)
        {
            _dbContext.Set<Review>().Update(review);
            await _dbContext.SaveChangesAsync();
        }
    }
}
