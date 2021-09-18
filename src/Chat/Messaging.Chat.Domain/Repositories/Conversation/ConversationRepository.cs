using Messaging.Chat.Domain.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Messaging.Chat.Domain.Repositories
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly IMongoDatabase _database;
        public ConversationRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Conversation> GetAsync(Guid senderId, Guid receiverId)
        {
            List<Guid> part = new List<Guid> { senderId, receiverId };
            var result = Collection.AsQueryable()
             .AsEnumerable()
            .FirstOrDefault(b => b.Participants.All(s => part.Contains(s)));

            return result;
        }
        public async Task<List<Conversation>> GetUserConversationsAsync(Guid userId)
        {
            var filter = Builders<Conversation>.Filter
                    .ElemMatch(z => z.Participants, a => a == userId);

            var result = await Collection
                .Find(filter)
                .ToListAsync();

            return result;
        }
        public async Task InsertAsync(Conversation conversation)

            => await Collection.InsertOneAsync(conversation);
        private IMongoCollection<Conversation> Collection
            => _database.GetCollection<Conversation>("Conversations");
    }
}
