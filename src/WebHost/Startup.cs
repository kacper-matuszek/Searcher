using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Searcher.Application.Abstractions.Products;
using Searcher.Common.Host;
using Searcher.Common.Host.Extensions;
using Searcher.Common.Host.HealthChecks;
using Searcher.Persistence.Mongo.Products;

namespace Searcher.WebHost;

public class Startup : BaseStartup
{
    public Startup(IHostEnvironment host)
        : base(host) { }

    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);
        services.AddMongo(Configuration, typeof(IProductRepository).Assembly);

        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddHealthChecks()
            .AddCheck<MongoHealthCheck>(nameof(MongoHealthCheck));
    }

    public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        base.Configure(app, env);
        app.UseMongo();
    }
}