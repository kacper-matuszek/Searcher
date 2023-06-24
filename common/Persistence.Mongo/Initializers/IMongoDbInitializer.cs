using System.Threading.Tasks;

namespace Searcher.Common.Persistence.Mongo.Initializers;

public interface IMongoDbInitializer
{
    Task Initialize();
}