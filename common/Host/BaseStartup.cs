using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Searcher.Common.Host.Extensions;
using Searcher.Persistence.Mongo;

namespace Searcher.Common.Host;

public abstract class BaseStartup : IStartup
{
    public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoOptions>(MongoOptions.SectionName, configuration);
    }

    public virtual void ConfigureConfiguration(
        IConfigurationBuilder configurationBuilder,
        IHostEnvironment host)
    {
        configurationBuilder.AddAppSettingsConfiguration(host);
        configurationBuilder.AddLogsConfiguration(host);
    }

    public void ConfigureApp(IApplicationBuilder applicationBuilder)
    {

    }
}