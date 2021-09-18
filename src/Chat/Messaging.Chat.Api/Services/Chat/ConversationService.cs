using Messaging.Chat.Domain.Models;
using Messaging.Chat.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messaging.Chat.Api.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly ILogger<ConversationService> _logger;
        public ConversationService(IConversationRepository conversationRepository, ILogger<ConversationService> logger)
        {
            _conversationRepository = conversationRepository;
            _logger = logger;
        }
        public async Task<Guid> CreateConversation(Guid senderId, Guid receiverId)
        {
            _logger.LogInformation("Getting conversationId between senderId and receiverId");
            var conversation = await _conversationRepository.GetAsync(senderId, receiverId);

            if (conversation == null)
            {
                conversation = new Conversation(senderId, receiverId);
                await _conversationRepository.InsertAsync(conversation);
            }
            _logger.LogInformation($"Created  conversationId :'${conversation.Id}' between senderId and receiverId");

            return conversation.Id;
        }

        public async Task<List<Conversation>> GetUserConservations(Guid userId)
        {
            _logger.LogInformation($"Getting  '${userId}' user's conversations.");

            var conversation = await _conversationRepository.GetUserConversationsAsync(userId);

            _logger.LogInformation($"Getting  '${userId}' user's conversations.");

            return conversation;
        }
    }
}
