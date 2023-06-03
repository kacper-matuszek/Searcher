using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Searcher.Common.Host;

public interface IStartup
{
    public IServiceCollection ConfigureServices(IServiceCollection services, IConfiguration configuration);
    public IConfigurationBuilder ConfigureConfiguration(IConfigurationBuilder configurationBuilder, IHostEnvironment host);
}