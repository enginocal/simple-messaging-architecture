using Messaging.Chat.Domain.Models;
using System.Threading.Tasks;

namespace Messaging.Chat.Services.Services
{
    public interface IMessageService
    {
        Task Save(Message message);
    }
}
