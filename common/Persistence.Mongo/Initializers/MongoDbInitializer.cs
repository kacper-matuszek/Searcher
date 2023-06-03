using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;

namespace Searcher.Persistence.Mongo.Initializers;

public class MongoDbInitializer : IMongoDbInitializer
{
    private static int _isInitialized = 0;

    public Task Initialize()
    {
        if (Interlocked.Exchange(ref _isInitialized, 1) == 1)
        {
            return Task.CompletedTask;
        }

        RegisterConventions();
        return Task.CompletedTask;
    }

    private static void RegisterConventions()
    {
        //Explanation decimal serialization: https://stackoverflow.com/questions/66802866/why-doesnt-mongodb-c-sharp-driver-use-bsontype-decimal128-representation-for-de        
        var decimalSerializer = new DecimalSerializer(
            representation: BsonType.Decimal128,
            converter: new RepresentationConverter(allowOverflow: false, allowTruncation: false));

        BsonSerializer.RegisterSerializer(typeof(decimal), decimalSerializer);
        BsonSerializer.RegisterSerializer(typeof(decimal?), new NullableSerializer<decimal>(decimalSerializer));

        //Explanation guid serialization: https://jira.mongodb.org/browse/CSHARP-3179
        //https://mongodb.github.io/mongo-csharp-driver/2.12/reference/bson/guidserialization/guidrepresentationmode/guidrepresentationmode/
        var guidSerializer = new GuidSerializer(GuidRepresentation.Standard);
        BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;
        BsonSerializer.RegisterSerializer(guidSerializer);
        BsonSerializer.RegisterSerializer(new NullableSerializer<Guid>(guidSerializer));
        ConventionRegistry.Register("conventions", new MongoDbConventions(), _ => true);
    }

    private class MongoDbConventions : IConventionPack
    {
        public IEnumerable<IConvention> Conventions => new List<IConvention>
        {
            new IgnoreExtraElementsConvention(true),
            new EnumRepresentationConvention(BsonType.String),
            new CamelCaseElementNameConvention()
        };
    }
}
