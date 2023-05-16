using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Searcher.Host;

public interface IStartup
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration);
}
