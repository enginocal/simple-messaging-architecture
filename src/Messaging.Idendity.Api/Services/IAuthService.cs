using System.Threading.Tasks;
using Messaging.Common.Auth;

namespace Messaging.Services.Identity.Services
{
    public interface ICustomAuthService
    {
         Task<JsonWebToken> LoginAsync(string email, string password);
    }
}