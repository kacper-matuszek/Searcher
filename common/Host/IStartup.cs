using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Searcher.Common.Host;

public interface IStartup
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration);
    public void ConfigureConfiguration(IConfigurationBuilder configurationBuilder, IHostEnvironment host);
}