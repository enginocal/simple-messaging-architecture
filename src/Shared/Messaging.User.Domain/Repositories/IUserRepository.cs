using Messaging.Users.Domain.Models;
using System;
using System.Threading.Tasks;


namespace Messaging.Users.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task<User> GetByUserNameAsync(string userName);
        Task AddAsync(User user);
        Task BlockUser(Guid userId, BlockedUser blockUser);
    }
}