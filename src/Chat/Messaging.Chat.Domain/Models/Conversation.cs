using Messaging.Common.Exceptions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;

namespace Messaging.Chat.Domain.Models
{
    public class Conversation
    {
        [BsonId(IdGenerator = typeof(GuidGenerator))]
        [BsonRepresentation(BsonType.String)]
        [BsonIgnoreIfDefault]
        public Guid Id { get; protected set; }
        public DateTime Created { get; protected set; }
        public List<Guid> Participants { get; protected set; }

        public Conversation(Guid senderId, Guid receiverId)
        {

            if (senderId == Guid.Empty)
            {
                throw new HomeRunException("empty_sender", "Sender can not be empty.");
            }
            if (receiverId == Guid.Empty)
            {
                throw new HomeRunException("empty_receiver", "Receiver can not be empty.");
            }
            
            Id = Guid.NewGuid();
            Participants = new List<Guid>()
            {
                senderId,receiverId
            };
            Created = DateTime.Now;
        }
    }
}
