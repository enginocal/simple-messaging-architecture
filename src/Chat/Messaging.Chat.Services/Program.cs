using Messaging.Chat.Application.Commands;
using Messaging.Common.Services;

namespace Messaging.Chat.Services
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args)
                            .UseRabbitMq()
                            .SubscribeToCommand<CreateMessage>()
                            .Build()
                            .Run();
        }
    }
}
