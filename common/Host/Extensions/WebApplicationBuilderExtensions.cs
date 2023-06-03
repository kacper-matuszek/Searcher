using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Searcher.Common.Host.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplication BuildUsingStartup<TStartup>(
        this WebApplicationBuilder builder,
        Func<IHostEnvironment, TStartup> createFunc)
        where TStartup : class, IStartup
    {
        ConfigureHostEnvironment(builder.Environment);

        var startup = createFunc(builder.Environment);
        startup.ConfigureServices(builder.Services);

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(startup.Configuration)
            .CreateLogger();
        builder.Host.UseSerilog((ctx, cfg) => cfg.ReadFrom.Configuration(startup.Configuration));

        var app = builder.Build();
        startup.Configure(app, builder.Environment);

        return app;
    }

    private static void ConfigureHostEnvironment(IHostEnvironment host)
    {
        host.ApplicationName = "Searcher";
        host.EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!;
    }
}
