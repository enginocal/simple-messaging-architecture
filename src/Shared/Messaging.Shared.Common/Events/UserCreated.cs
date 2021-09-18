namespace Messaging.Common.Events
{
    public class UserCreated : IEvent
    {
        public string Email { get; }
        public string Name { get; }
        public string UserName { get; set; }

        protected UserCreated()
        {
        }

        public UserCreated(string email, string name, string userName)
        {
            Email = email;
            Name = name;
            UserName = userName;
        }
    }
}