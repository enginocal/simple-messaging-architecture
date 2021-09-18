using Messaging.Common.Commands;
using Messaging.Common.Services;
using Messaging.Shared.Common.Commands;

namespace Messaging.Services.Users
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args)
                .UseRabbitMq()
                .SubscribeToCommand<CreateUser>()
                .SubscribeToCommand<BlockUser>()
                .Build()
                .Run();
        }
    }
}