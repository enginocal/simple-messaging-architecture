using System.Threading.Tasks;
using Messaging.Common.Auth;
using Messaging.Common.Exceptions;
using Messaging.Services.Identity.Domain.Services;
using Messaging.Users.Domain.Repositories;

namespace Messaging.User.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IEncrypter _encrypter;
        private readonly IJwtHandler _jwtHandler;

        public UserService(IUserRepository repository,
            IEncrypter encrypter,
            IJwtHandler jwtHandler)
        {
            _repository = repository;
            _encrypter = encrypter;
            _jwtHandler = jwtHandler;
        }

        public async Task RegisterAsync(string email, string password, string name)
        {
            var user = await _repository.GetAsync(email);
            if (user != null)
            {
                throw new MessagingException("email_in_use", $"Email: '{email}' is already in use.");
            }
            user = new Messaging.Users.Domain.Models.User(email, name);
            user.SetPassword(password, _encrypter);
            await _repository.AddAsync(user);
        }
    }
}