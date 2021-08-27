using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
  public  interface IUserRepository: IAsyncRepository<User>
    {
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserPurchaseById(int id);
        Task<User> GetUserFavoriteById(int id);
        Task<Favorite> AddAsync(Favorite favorite);
        Task<Favorite> Remove(Favorite favorite);
        Task AddAsync(Review review);
        Task<Review> getReviewByIdAsync(int movieId, int userId);
        Task UpdateAsync(Review review);
    }
}
