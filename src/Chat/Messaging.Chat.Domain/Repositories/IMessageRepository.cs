using Messaging.Chat.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messaging.Chat.Domain.Repositories
{
    public interface IMessageRepository
    {
        Task InsertAsync(Message message);
        Task<IEnumerable<Message>> GetByUser(Guid userId);
        Task<IEnumerable<Message>> GetByConversationId(Guid conversationId);
    }
}
