using System;
using Microsoft.Extensions.Hosting;

namespace Searcher.Common.Host.Extensions;

public static class HostBuilderExtensions
{
    public static IHostBuilder UseStartup<TStartup>(this IHostBuilder hostBuilder)
        where TStartup : IStartup, new()
    {
        var startup = new TStartup();

        return hostBuilder
            .ConfigureAppConfiguration((context, configurationBuilder) =>
            {
                ConfigureHostEnvironment(context.HostingEnvironment);
                startup.ConfigureConfiguration(configurationBuilder, context.HostingEnvironment);
            })
            .ConfigureServices((context, services) => startup.ConfigureServices(services, context.Configuration));
    }

    private static void ConfigureHostEnvironment(IHostEnvironment host)
    {
        host.ApplicationName = "Searcher";
        host.EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!;
    }
}
