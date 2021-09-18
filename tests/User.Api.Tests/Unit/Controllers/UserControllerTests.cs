using System;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentAssertions;
using Messaging.Shared.Common.Commands;
using Messaging.User.Api.Controllers;
using Messaging.User.Api.Model;
using Messaging.Users.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RawRabbit;
using Xunit;

namespace User.Api.Tests.Controllers
{
    public class UserControllerTests
    {
        [Fact]
        public async Task user_controller_block_user_should_return_accepted()
        {
            var busClientMock = new Mock<IBusClient>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var controller = new UsersController(busClientMock.Object,
                userRepositoryMock.Object);
            var userId = Guid.NewGuid();
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, userId.ToString())
                        }, "test"))
                }
            };

            var command = new BlockUserRequest
            {
                UserId = userId,
                BlockedUserName = "isa"
            };

            var result = await controller.BlockUser(command);

            var contentResult = result as AcceptedResult;
            contentResult.Should().NotBeNull();
        }
    }
}