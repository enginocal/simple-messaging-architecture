using Messaging.Common.Commands;
using System;

namespace Messaging.Shared.Common.Commands
{
    public class BlockUser : ICommand
    {
        public Guid UserId { get; set; }
        public Guid BlockedUserId { get; set; }
    }
}
