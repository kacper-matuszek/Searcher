using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Searcher.Common.Host.Extensions;

namespace Searcher.Common.Host;

public abstract class BaseStartup : IStartup
{
    protected BaseStartup(IHostEnvironment host)
    {
        Configuration = new ConfigurationBuilder()
            .AddAppSettingsConfiguration(host)
            .AddLogsConfiguration(host)
            .Build();
    }

    public IConfiguration Configuration { get; } 

    public virtual void ConfigureServices(IServiceCollection services)
    {
        services.AddMongo(Configuration);
    }

    public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseMongo();
    }
}