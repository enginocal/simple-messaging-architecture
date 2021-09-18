using Messaging.Chat.Application.Commands;
using Messaging.Chat.Domain.Models;
using Messaging.Chat.Services.Services;
using Messaging.Common.Commands;
using Messaging.Common.Exceptions;
using Microsoft.Extensions.Logging;
using RawRabbit;
using System;
using System.Threading.Tasks;

namespace Messaging.User.Services.Handlers
{
    public class CreateMessageHandler : ICommandHandler<CreateMessage>
    {
        private readonly ILogger _logger;
        private readonly IBusClient _busClient;
        private readonly IMessageService _messageService;

        public CreateMessageHandler(IBusClient busClient, IMessageService messageService, ILogger<CreateMessage> logger)
        {
            _busClient = busClient;
            _messageService = messageService;
            _logger = logger;
        }

        public async Task HandleAsync(CreateMessage command)
        {
            _logger.LogInformation($"Send message from : '{command.SenderId}' to conversationId: '{command.ConversationId}' context :'{command.Text}'");
            try
            {
                var msg = new Message(command.ConversationId, command.SenderId, command.Text);
                await _messageService.Save(msg);
                _logger.LogInformation($"Message Sent: '{command.Id}'");
                return;
            }
            catch (HomeRunException ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

    }
}
