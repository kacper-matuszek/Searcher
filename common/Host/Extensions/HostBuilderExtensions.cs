using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Searcher.Common.Host.Extensions;

public static class HostBuilderExtensions
{
    public static IHost BuildUsingStartup<TStartup>(
        this IHostBuilder hostBuilder)
        where TStartup : IStartup, new()
    {
        var startup = new TStartup();
        hostBuilder
            .ConfigureAppConfiguration((context, configurationBuilder) =>
            {
                ConfigureHostEnvironment(context.HostingEnvironment);
                startup.ConfigureConfiguration(configurationBuilder, context.HostingEnvironment);
            })
            .AddLogging()
            .ConfigureServices((context, services) => startup.ConfigureServices(services, context.Configuration));

        var host = hostBuilder.Build();

        startup.ConfigureApp(new ApplicationBuilder(host.Services));

        return host;
    }

    private static void ConfigureHostEnvironment(IHostEnvironment host)
    {
        host.ApplicationName = "Searcher";
        host.EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!;
    }

    private static IHostBuilder AddLogging(this IHostBuilder hostBuilder)
    {
        hostBuilder.ConfigureLogging((ctx, _) =>
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(ctx.Configuration)
                .CreateLogger();
        }).UseSerilog((ctx, cfg) => cfg.ReadFrom.Configuration(ctx.Configuration));

        return hostBuilder;
    }
}
