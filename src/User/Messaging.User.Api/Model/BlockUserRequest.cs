using System;

namespace Messaging.User.Api.Model
{
    public class BlockUserRequest
    {
        public Guid UserId { get; set; }
        public string BlockedUserName { get; set; }
    }
}
