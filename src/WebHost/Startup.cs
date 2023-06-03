using Microsoft.Extensions.Hosting;
using Searcher.Common.Host;

namespace Searcher.WebHost;

public class Startup : BaseStartup
{
    public Startup(IHostEnvironment host)
        : base(host) { }
}