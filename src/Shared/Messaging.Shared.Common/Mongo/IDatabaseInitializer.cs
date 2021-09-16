using System.Threading.Tasks;

namespace Messaging.Common.Mongo
{
    public interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
}