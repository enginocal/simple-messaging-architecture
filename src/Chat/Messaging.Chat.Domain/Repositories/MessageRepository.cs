using Messaging.Chat.Domain.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messaging.Chat.Domain.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IMongoDatabase _database;
        public MessageRepository(IMongoDatabase database)
        {
            _database = database;
        }
        public async Task InsertAsync(Message message)
        => await Collection.InsertOneAsync(message);

        public async Task<IEnumerable<Message>> GetByUser(Guid userId)
        {
            var filter = Builders<Message>.Filter.Eq(x => x.Sender, userId);
            var res = await Collection.FindAsync(filter).ConfigureAwait(true);
            var results = await res.ToListAsync().ConfigureAwait(false);
            return results;
        }

        public async Task<IEnumerable<Message>> GetByConversationId(Guid conversationId)
        {
            var result = await Collection.Find(x => x.ConversationId == conversationId).ToListAsync();
            return result;
        }

        private IMongoCollection<Message> Collection
            => _database.GetCollection<Message>("Messages");
    }
}
