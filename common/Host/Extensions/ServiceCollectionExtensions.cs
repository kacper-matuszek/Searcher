using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Searcher.Persistence.Mongo;
using Searcher.Persistence.Mongo.Initializers;

namespace Searcher.Common.Host.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoOptions>(configuration.GetSection(MongoOptions.SectionName));
        services.AddSingleton<IMongoClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<MongoOptions>>().Value;
            return new MongoClient(ConnectionStringBuilder.Build(options));
        });
        services.AddTransient<IMongoDbInitializer, MongoDbInitializer>(sp => new MongoDbInitializer());
        services.AddTransient<IMongoDatabase>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<MongoOptions>>().Value;
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(options.DatabaseName);
        });

        return services;
    }
}