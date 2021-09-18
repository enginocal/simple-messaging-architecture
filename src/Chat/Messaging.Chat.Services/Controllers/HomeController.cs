using Microsoft.AspNetCore.Mvc;

namespace Messaging.Chat.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get() => Content("Hello from Chat.Service Api!");

    }
}
