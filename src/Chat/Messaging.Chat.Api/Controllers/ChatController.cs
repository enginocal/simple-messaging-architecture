using Messaging.Chat.Api.Models;
using Messaging.Chat.Api.Services;
using Messaging.Chat.Application.Commands;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using System;
using System.Threading.Tasks;

namespace Messaging.Chat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatController : ControllerBase
    {
        private readonly IBusClient _busClient;
        private readonly IConversationService _chatService;

        public ChatController(IBusClient busClient, IConversationService chatService)
        {
            _busClient = busClient;
            _chatService = chatService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] Message message)
        {
            var conversationId = await _chatService.CreateConversation(message.Sender, message.Receiver);
            var command = new CreateMessage
            {
                Id = Guid.NewGuid(),
                ConversationId = conversationId,
                Created = DateTime.Now,
                SenderId = message.Sender,
                Text = message.Content
            };

            await _busClient.PublishAsync(command);

            return Accepted();
        }

        [HttpGet("Conversations")]
        public async Task<IActionResult> GetConversations(Guid userId)
        {
            var conversations = await _chatService.GetUserConservations(userId);
            return Ok(conversations);
        }

        [HttpGet("Message")]
        public async Task<IActionResult> GetMessages(Guid conversationId)
        {
            var conversations = await _chatService.GetUserConservations(conversationId);
            return Ok(conversations);
        }
    }
}
