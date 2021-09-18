using Messaging.Chat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messaging.Chat.Api.Services
{
    public interface IConversationService
    {
        Task<Guid> CreateConversation(Guid senderId, Guid receiverId);
        Task<List<Conversation>> GetUserConservations(Guid userId);
    }
}
