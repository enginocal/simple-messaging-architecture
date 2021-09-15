using System.Threading.Tasks;
using Messaging.Common.Auth;
using Messaging.Common.Exceptions;
using Messaging.Services.Identity.Domain.Services;
using Messaging.Users.Domain.Repositories;

namespace Messaging.Services.Identity.Services
{
    public class AuthService : ICustomAuthService
    {
        private readonly IUserRepository _repository;
        private readonly IEncrypter _encrypter;
        private readonly IJwtHandler _jwtHandler;

        public AuthService(IUserRepository repository,
            IEncrypter encrypter,
            IJwtHandler jwtHandler)
        {
            _repository = repository;
            _encrypter = encrypter;
            _jwtHandler = jwtHandler;
        }

        public async Task<JsonWebToken> LoginAsync(string email, string password)
        {
            var user = await _repository.GetAsync(email);
            if (user == null)
            {
                throw new MessagingException("invalid_credentials", $"Invalid credentials.");
            }
            if (!user.ValidatePassword(password, _encrypter))
            {
                throw new MessagingException("invalid_credentials", $"Invalid credentials.");
            }

            return _jwtHandler.Create(user.Id);
        }
    }
}