using Messaging.Common.Commands;
using System;

namespace Messaging.Chat.Application.Commands
{
    public class CreateMessage : ICommand
    {
        public Guid Id { get; set; }
        public Guid ConversationId { get; set; }
        public Guid SenderId { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
    }
}
