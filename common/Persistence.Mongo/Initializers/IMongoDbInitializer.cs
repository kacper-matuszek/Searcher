using System.Threading.Tasks;

namespace Searcher.Persistence.Mongo.Initializers;

public interface IMongoDbInitializer
{
    Task Initialize();
}