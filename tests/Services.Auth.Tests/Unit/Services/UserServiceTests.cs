using System;
using System.Threading.Tasks;
using FluentAssertions;
using Messaging.Common.Auth;
using Messaging.Services.Identity.Domain.Services;
using Messaging.Services.Identity.Services;
using Messaging.User.Services;
using Messaging.Users.Domain.Models;
using Messaging.Users.Domain.Repositories;
using Moq;
using Xunit;

namespace Services.Identity.Tests.Unit.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task user_service_login_should_return_jwt()
        {
            var email = "test@test.com";
            var password = "secret";
            var name = "test";
            var username = "engin";
            var salt = "salt";
            var hash = "hash";
            var token = "token";
            var userRepositoryMock = new Mock<IUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            var jwtHandlerMock = new Mock<IJwtHandler>();
            encrypterMock.Setup(x => x.GetSalt()).Returns(salt);
            encrypterMock.Setup(x => x.GetHash(password, salt)).Returns(hash);
            jwtHandlerMock.Setup(x => x.Create(It.IsAny<Guid>())).Returns(new JsonWebToken
            {
                Token = token
            });

            var user = new User(email, name, username);
            user.SetPassword(password, encrypterMock.Object);
            userRepositoryMock.Setup(x => x.GetAsync(email)).ReturnsAsync(user);

            var authService = new AuthService(userRepositoryMock.Object,
                encrypterMock.Object, jwtHandlerMock.Object);



            var jwt = await authService.LoginAsync(email, password);
            userRepositoryMock.Verify(x => x.GetAsync(email), Times.Once);
            jwtHandlerMock.Verify(x => x.Create(It.IsAny<Guid>()), Times.Once);
            jwt.Should().NotBeNull();
            jwt.Token.ShouldBeEquivalentTo(token);
        }
    }
}