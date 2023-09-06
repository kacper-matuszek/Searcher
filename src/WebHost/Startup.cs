using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Searcher.Application.Abstractions.Products;
using Searcher.Common.Host;
using Searcher.Persistence.Mongo.Products;

namespace Searcher.WebHost;

public class Startup : BaseStartup
{
    public Startup(IHostEnvironment host)
        : base(host) { }

    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);
        services.AddScoped<IProductRepository, ProductRepository>();
    }
}