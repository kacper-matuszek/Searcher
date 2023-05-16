using Microsoft.Extensions.Hosting;

namespace Searcher.Host.Extensions;

public static class HostBuilderExtensions
{
    public static IHostBuilder UseStartup<TStartup>(this IHostBuilder hostBuilder)
        where TStartup : IStartup, new()
    {
        var startup = new TStartup();

        hostBuilder.ConfigureServices((context, services) => startup.ConfigureServices(services, context.Configuration));

        return hostBuilder;
    }
}
