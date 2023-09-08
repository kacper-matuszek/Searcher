using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Searcher.Common.Host.Extensions;
using Searcher.Common.Host.HealthChecks;

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

    }

    public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecks("health", new()
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json; charset=utf-8";
                    var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(report));
                    await context.Response.Body.WriteAsync(bytes);
                }
            });
        });
    }
}