using Messaging.Chat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messaging.Chat.Api.Services
{
    public interface IMessageService
    {
        Task<Guid> CreateConversation(Guid senderId, Guid receiverId);
        Task<IEnumerable<Message>> GetMessageByUser(Guid userId);
    }
}
