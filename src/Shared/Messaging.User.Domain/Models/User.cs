using System;
using System.Collections.Generic;
using Messaging.Common.Exceptions;
using Messaging.Services.Identity.Domain.Services;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Messaging.Users.Domain.Models
{
    public class User
    {
        [BsonId(IdGenerator = typeof(GuidGenerator))]
        [BsonRepresentation(BsonType.String)]
        [BsonIgnoreIfDefault]
        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string Name { get; protected set; }
        public string UserName { get; set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        public List<BlockedUser> BlockedUsers { get; protected set; }

        protected User()
        {
        }

        public User(string email, string name, string username)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new HomeRunException("empty_user_email", "User email can not be empty.");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new HomeRunException("empty_name", "User name can not be empty.");
            }
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new HomeRunException("empty_user_name", "User name can not be empty.");
            }
            Id = Guid.NewGuid();
            Email = email.ToLowerInvariant();
            Name = name;
            CreatedAt = DateTime.UtcNow;
            UserName = username;
            BlockedUsers = new List<BlockedUser>();
        }

        public void SetPassword(string password, IEncrypter encrypter)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new HomeRunException("empty_password", "Password can not be empty.");
            }
            Salt = encrypter.GetSalt();
            Password = encrypter.GetHash(password, Salt);
        }

        public bool ValidatePassword(string password, IEncrypter encrypter)
            => Password.Equals(encrypter.GetHash(password, Salt));
    }

    public class BlockedUser
    {
        [BsonId(IdGenerator = typeof(GuidGenerator))]
        [BsonRepresentation(BsonType.String)]
        [BsonIgnoreIfDefault]
        public Guid UserId { get; set; }
        public DateTime BlockedTime { get; set; }
    }
}