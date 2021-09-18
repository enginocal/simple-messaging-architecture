using System;

namespace Messaging.Chat.Api.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid ConversationId { get; set; }
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public string Content { get; set; }
    }
}
