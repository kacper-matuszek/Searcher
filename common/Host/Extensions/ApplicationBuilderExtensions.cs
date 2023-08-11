using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Searcher.Common.Persistence.Mongo.Initializers;

namespace Searcher.Common.Host.Extensions;

internal static class ApplicationBuilderExtensions
{
    internal static IApplicationBuilder UseMongo(this IApplicationBuilder applicationBuilder)
    {
        var mongoInitializer = applicationBuilder.ApplicationServices.GetRequiredService<IMongoDbInitializer>();

        mongoInitializer.Initialize().Wait();

        return applicationBuilder;
    }
}