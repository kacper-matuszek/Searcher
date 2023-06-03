using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Searcher.Persistence.Mongo;

namespace Searcher.Common.Host.HealthChecks;

public class MongoHealthCheck : IHealthCheck
{
    private readonly IMongoClient _client;
    private readonly MongoOptions _options;

    public MongoHealthCheck(IMongoClient client, IOptions<MongoOptions> options)
    {
        _client = client;
        _options = options.Value;
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        return CheckConnection();
    }

    private async Task<HealthCheckResult> CheckConnection()
    {
        try
        {
            var db = _client.GetDatabase(_options.DatabaseName);
            await db.RunCommandAsync((Command<BsonDocument>)"{ping:1}");
        }
        catch (Exception)
        {
            return HealthCheckResult.Unhealthy();
        }

        return HealthCheckResult.Healthy();
    }
}