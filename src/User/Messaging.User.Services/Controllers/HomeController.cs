using Microsoft.AspNetCore.Mvc;

namespace Messaging.User.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get() => Content("Hello from User.Service Api!");

    }
}
