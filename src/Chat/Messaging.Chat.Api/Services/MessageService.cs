using Messaging.Chat.Domain.Models;
using Messaging.Chat.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messaging.Chat.Api.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IConversationRepository _conversationRepository;
        private readonly ILogger _logger;

        public MessageService(ILogger<MessageService> logger, IConversationRepository conversationRepository, IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
            _conversationRepository = conversationRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Message>> GetMessageByUser(Guid userId)
        {
            _logger.LogInformation($"Getting user's '${userId}' messages.");
            List<Message> result;
            try
            {
                result = (List<Message>)await _messageRepository.GetByUser(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured getting user's messages", ex);
                throw;
            }

            return result;
        }

        public async Task<Guid> CreateConversation(Guid senderId, Guid receiverId)
        {
            Conversation conversation;
            _logger.LogInformation($"Creating conversation between '${senderId}' and '${receiverId}' participitants.");
            try
            {
                conversation = new Conversation(senderId, receiverId);
                await _conversationRepository.InsertAsync(conversation);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured creating conversation from user", ex);
                throw;
            }

            return conversation.Id;
        }
    }
}
