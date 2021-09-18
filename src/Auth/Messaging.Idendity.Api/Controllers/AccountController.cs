using Messaging.Common.Commands;
using Messaging.Services.Identity.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Messaging.Idendity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ICustomAuthService _authService;

        public AccountController(ICustomAuthService userService)
        {
            _authService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateUser command)
            => Ok(await _authService.LoginAsync(command.Email, command.Password));

    }
}
