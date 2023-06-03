using Microsoft.Extensions.Hosting;
using Searcher.Common.Host.Extensions;
using Searcher.Host;

var host = Host.CreateDefaultBuilder(args)
    .BuildUsingStartup<Startup>();

await host.RunAsync();