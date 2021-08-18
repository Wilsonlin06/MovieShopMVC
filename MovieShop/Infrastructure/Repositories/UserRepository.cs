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
    }
}
