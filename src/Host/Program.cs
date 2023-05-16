using Microsoft.Extensions.Hosting;
using Searcher.Host;
using Searcher.Host.Extensions;

var host = Host.CreateDefaultBuilder(args)
    .UseStartup<Startup>()
    .Build();

await host.RunAsync();