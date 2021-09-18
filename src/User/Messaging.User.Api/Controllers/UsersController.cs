using Messaging.Common.Commands;
using Messaging.Shared.Common.Commands;
using Messaging.User.Api.Model;
using Messaging.Users.Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using System.Threading.Tasks;

namespace Messaging.User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private readonly IBusClient _busClient;
        private readonly IUserRepository _userRepository;

        public UsersController(IBusClient busClient, IUserRepository userRepository)
        {
            _busClient = busClient;
            _userRepository = userRepository;
        }

        [HttpPost("")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] CreateUser command)
        {
            await _busClient.PublishAsync(command);

            return Accepted();
        }

        [Authorize]
        [HttpGet("{userName}")]
        public async Task<IActionResult> GetUserIdByUserName(string userName)
        {
            var user = await _userRepository.GetByUserNameAsync(userName);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                user.Id,
                user.Email,
                user.Name,
                user.UserName
            });
        }

        [Authorize]
        [HttpPost("Block")]
        public async Task<IActionResult> BlockUser([FromBody] BlockUserRequest request)
        {

            var user = await _userRepository.GetByUserNameAsync(request.BlockedUserName);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var command = new BlockUser
            {
                UserId = request.UserId,
                BlockedUserId = user.Id
            };
            await _busClient.PublishAsync(command);

            return Accepted();
        }

    }
}
