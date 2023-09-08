using MongoDB.Bson.Serialization;

namespace Searcher.Common.Persistence.Mongo;

public interface IDocumentConfiguration<TDocument>
    where TDocument : class
{
    void Configure(BsonClassMap<TDocument> map);
}
