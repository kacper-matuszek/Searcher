using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Searcher.Common.Host;

public interface IStartup
{
    public IConfiguration Configuration { get; }
    public void ConfigureServices(IServiceCollection services);
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env);
}