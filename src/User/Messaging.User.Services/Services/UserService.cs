using System;
using System.Threading.Tasks;
using Messaging.Common.Exceptions;
using Messaging.Services.Identity.Domain.Services;
using Messaging.Users.Domain.Repositories;

namespace Messaging.User.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IEncrypter _encrypter;

        public UserService(IUserRepository repository, IEncrypter encrypter)
        {
            _repository = repository;
            _encrypter = encrypter;
        }

        public async Task BlockUserAsync(Guid userId, Guid blockedId)
        {
            await _repository.BlockUser(userId, new Users.Domain.Models.BlockedUser { UserId = blockedId, BlockedTime = DateTime.Now });
        }

        public async Task RegisterAsync(string email, string password, string name, string userName)
        {

            var user = await _repository.GetAsync(email);
            if (user != null)
            {
                throw new HomeRunException("email_in_use", $"Email: '{email}' is already in use.");
            }
            user = new Messaging.Users.Domain.Models.User(email, name, userName);
            user.SetPassword(password, _encrypter);
            await _repository.AddAsync(user);
        }
    }
}