using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Runtime.Documents;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;

namespace Searcher.Common.Persistence.Mongo.Initializers;

internal sealed class MongoDbInitializer : IMongoDbInitializer
{
    private static int _isInitialized = 0;
    private readonly Assembly[] _assemblies;

    public MongoDbInitializer(Assembly[] assemblies)
    {
        _assemblies = assemblies;
    }

    public Task Initialize()
    {
        if (Interlocked.Exchange(ref _isInitialized, 1) == 1)
        {
            return Task.CompletedTask;
        }

        RegisterConventions();
        RegisterDocumentConfigurations(_assemblies);
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

    private static void RegisterDocumentConfigurations(Assembly[] assemblies)
    {
        const string registerClassMapName = "RegisterClassMap";

        var documentInterfaceType = typeof(IDocumentConfiguration<>);
        var configurationTypes = assemblies.SelectMany(a => a.GetTypes())
            .Where(a => a.GetInterface(documentInterfaceType.Name) != null)
            .Select(a => (Configuration: a, DocumentType: a.GetInterface(documentInterfaceType.Name)!.GetGenericArguments().First()));

        foreach((var configurationType, var documentType) in configurationTypes)
        {
            if (BsonClassMap.IsClassMapRegistered(documentType))
                continue;

            var documentConfigurator = Activator.CreateInstance(documentType);
            if (documentConfigurator is null)
                continue;

            var configuredMethod = configurationType.GetMethod(nameof(IDocumentConfiguration<object>.Configure))!;
            var genericBsonClassMapType = typeof(BsonClassMap<>).MakeGenericType(documentType);
            var actionType = Expression.GetActionType(genericBsonClassMapType);
            var actionMap = Delegate.CreateDelegate(actionType, documentConfigurator, configuredMethod);
            var registerClassMapMethod = typeof(BsonClassMap).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .First(m => m.Name == registerClassMapName && m.IsGenericMethod && m.GetParameters().Length == 1)
                .MakeGenericMethod(new[] { documentType });
            registerClassMapMethod.Invoke(documentConfigurator, new object[] {actionMap});
        }
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
