using FluentAssertions;
using Messaging.User.Services.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace User.Api.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void home_controller_get_should_return_string_content()
        {
            var controller = new HomeController();

            var result = controller.Get();
            
            var contentResult = result as ContentResult;
            contentResult.Should().NotBeNull();
            //contentResult.Content.("Hello from API!");
        }
    }
}