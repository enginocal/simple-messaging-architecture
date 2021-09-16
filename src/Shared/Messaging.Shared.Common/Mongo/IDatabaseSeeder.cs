using System.Threading.Tasks;

namespace Messaging.Common.Mongo
{
    public interface IDatabaseSeeder
    {
         Task SeedAsync();
    }
}