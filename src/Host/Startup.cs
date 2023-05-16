using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Searcher.Host;

public class Startup : IStartup
{
    public void ConfigureConfiguration(IConfigurationBuilder configurationBuilder, IHostEnvironment host)
    {
        const string configurationFolder = "Configurations";

        host.EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!;

        configurationBuilder
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile($"{configurationFolder}/appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{configurationFolder}/appsettings.{host.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();
    }

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {

    }
}
