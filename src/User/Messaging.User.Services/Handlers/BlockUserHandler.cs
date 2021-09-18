using Messaging.Common.Commands;
using Messaging.Common.Exceptions;
using Messaging.Shared.Common.Commands;
using Microsoft.Extensions.Logging;
using RawRabbit;
using System;
using System.Threading.Tasks;

namespace Messaging.User.Services.Handlers
{
    public class BlockUserHandler : ICommandHandler<BlockUser>
    {
        private readonly ILogger _logger;
        private readonly IBusClient _busClient;
        private readonly IUserService _userService;

        public BlockUserHandler(IBusClient busClient, IUserService userService, ILogger<CreateUser> logger)
        {
            _busClient = busClient;
            _userService = userService;
            _logger = logger;
        }

        public async Task HandleAsync(BlockUser command)
        {
            _logger.LogInformation($"Blocking user: '{command.BlockedUserId}'");
            try
            {
                await _userService.BlockUserAsync(command.UserId, command.BlockedUserId);
                _logger.LogInformation($"User Blocked: '{command.BlockedUserId}'  '");
                return;
            }
            catch (HomeRunException ex)
            {
                _logger.LogError(ex, ex.Message);
                //await _busClient.PublishAsync(new CreateUserRejected(command.Email, ex.Message, ex.Code));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                //await _busClient.PublishAsync(new CreateUserRejected(command.Email, ex.Message, "error"));
            }
        }
    }
}
