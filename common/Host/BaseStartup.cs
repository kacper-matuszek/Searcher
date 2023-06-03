using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Searcher.Common.Host.Extensions;

namespace Searcher.Common.Host;

public abstract class BaseStartup : IStartup
{
    public virtual IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        return services;
    }

    public virtual IConfigurationBuilder ConfigureConfiguration(
        IConfigurationBuilder configurationBuilder,
        IHostEnvironment host)
    {
        configurationBuilder.AddAppSettingsConfiguration(host);
        configurationBuilder.AddLogsConfiguration(host);
        return configurationBuilder;
    }
}