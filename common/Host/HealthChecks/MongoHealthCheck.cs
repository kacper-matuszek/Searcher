using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Searcher.Common.Persistence.Mongo;

namespace Searcher.Common.Host.HealthChecks;

internal sealed class MongoHealthCheck : IHealthCheck
{
    private readonly IMongoDatabase _database;

    public MongoHealthCheck(IMongoClient client, IOptions<MongoOptions> options)
    {
        _database = client.GetDatabase(options.Value.DatabaseName);
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        var isStable = await CheckConnection();

        if (isStable)
            return HealthCheckResult.Healthy();

        return HealthCheckResult.Unhealthy("Cannot make stable connection with MongoDb");
    }

    private async Task<bool> CheckConnection()
    {
        try
        {
            var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(2));
            await _database.RunCommandAsync((Command<BsonDocument>)"{ping:1}", cancellationToken: cancellationTokenSource.Token);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }
}