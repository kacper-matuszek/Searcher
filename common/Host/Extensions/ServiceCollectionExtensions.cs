using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Searcher.Common.Persistence.Mongo;
using Searcher.Common.Persistence.Mongo.Initializers;
using System;
using System.Reflection;

namespace Searcher.Common.Host.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration, params Assembly[] assemblies)
    {
        if (assemblies.Length == 0)
            throw new ArgumentException("Assemblies array must have at least one assembly.");

        services.Configure<MongoOptions>(configuration.GetSection(MongoOptions.SectionName));
        services.AddSingleton<IMongoClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<MongoOptions>>().Value;
            return new MongoClient(ConnectionStringBuilder.Build(options));
        });
        services.AddTransient<IMongoDbInitializer, MongoDbInitializer>(sp => new MongoDbInitializer(assemblies));
        services.AddTransient<IMongoDatabase>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<MongoOptions>>().Value;
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(options.DatabaseName);
        });

        return services;
    }
}