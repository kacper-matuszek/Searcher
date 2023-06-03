using Microsoft.AspNetCore.Builder;
using Searcher.Common.Host.Extensions;
using Searcher.WebHost;

var app = WebApplication.CreateBuilder(args)
    .BuildUsingStartup(host => new Startup(host));

app.MapGet("/", () => "Hi!");

app.Run();
