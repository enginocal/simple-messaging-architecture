using System;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Messaging.Users.Domain.Models;

namespace Messaging.Users.Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase _database;
        public UserRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<User> GetAsync(Guid id)
            => await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == id);


        public async Task<User> GetAsync(string email)
            => await Collection
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Email == email.ToLowerInvariant());

        public async Task<User> GetByUserNameAsync(string username)
           => await Collection
               .AsQueryable()
               .FirstOrDefaultAsync(x => x.UserName == username.ToLowerInvariant());

        public async Task BlockUser(Guid userId, BlockedUser blockedUser)
        {
            var filter = Builders<User>.Filter.And(Builders<User>.Filter.Where(x => x.Id == userId));
            var builder = Builders<User>.Update;
            var update = builder.Push(x => x.BlockedUsers, blockedUser);
            await Collection.FindOneAndUpdateAsync(filter, update);
        }

        public async Task AddAsync(User user)
            => await Collection.InsertOneAsync(user);

        private IMongoCollection<User> Collection
            => _database.GetCollection<User>("Users");
    }
}