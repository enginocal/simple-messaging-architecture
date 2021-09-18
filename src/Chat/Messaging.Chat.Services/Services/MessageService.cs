using Messaging.Chat.Domain.Models;
using Messaging.Chat.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace Messaging.Chat.Services.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task Save(Message message)
        {
            await _messageRepository.InsertAsync(message);
        }
    }
}
