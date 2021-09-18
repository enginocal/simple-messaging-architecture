using Messaging.Common.Exceptions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;

namespace Messaging.Chat.Domain.Models
{
    public class Message
    {
        [BsonId(IdGenerator = typeof(GuidGenerator))]
        [BsonRepresentation(BsonType.String)]
        [BsonIgnoreIfDefault]
        public Guid Id { get; set; }
        public Guid ConversationId { get; set; }
        public Guid Sender { get; set; }
        public string Content { get; set; }

        protected Message()
        {

        }

        public Message(Guid conversationId, Guid senderId, string content)
        {
            if (conversationId == Guid.Empty)
            {
                throw new HomeRunException("empty_conversationId", "Conversation id can not be empty.");
            }
            if (conversationId == Guid.Empty)
            {
                throw new HomeRunException("empty_senderId", "Sender id can not be empty.");
            }
            if (string.IsNullOrEmpty(content))
            {
                throw new HomeRunException("empty_content", "Content can not be empty.");
            }
            Id = Guid.NewGuid();
            ConversationId = conversationId;
            Sender = senderId;
            Content = content;
        }
    }
}
