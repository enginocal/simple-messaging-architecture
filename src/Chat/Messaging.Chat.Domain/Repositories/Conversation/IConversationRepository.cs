using Messaging.Chat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messaging.Chat.Domain.Repositories
{
    public interface IConversationRepository
    {
        Task<Conversation> GetAsync(Guid senderId, Guid receiverId);
        Task InsertAsync(Conversation conversation);
        Task<List<Conversation>> GetUserConversationsAsync(Guid userId);

    }
}
