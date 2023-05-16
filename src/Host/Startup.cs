using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Searcher.Host;

public class Startup : IStartup
{
    public void ConfigureConfiguration(IConfigurationBuilder configurationBuilder, IHostEnvironment host)
    {

    }

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {

    }
}
