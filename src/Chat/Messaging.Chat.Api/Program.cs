using Messaging.Common.Services;

namespace Messaging.Chat.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args)
                            .UseRabbitMq()
                            .Build()
                            .Run();
        }
    }
}
