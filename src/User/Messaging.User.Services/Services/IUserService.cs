using System.Threading.Tasks;

namespace Messaging.User.Services
{
    public interface IUserService
    {
         Task RegisterAsync(string email, string password, string name);
    }
}