using FluentAssertions;
using NBomber.Contracts;
using NBomber.CSharp;
using NBomber.Http.CSharp;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace Searcher.Performance.Tests;

public class MainApiPerformanceTests
{
    private readonly ITestOutputHelper _outputHelper;

    public MainApiPerformanceTests(ITestOutputHelper outputHelper) => 
        _outputHelper = outputHelper;

    [Fact]
    public void Get_Hi_ShouldHandle_AtLeast_100RequestesPerSecond()
    {
        const string url = "http://localhost:5236/";

        const int expectedRequestsPerSecond = 100;
        const int duration = 5;

        var httpClient = new HttpClient()
        {
            BaseAddress = new Uri(url)
        };

        var getHiStep = Scenario.Create("get hi", async context =>
            await TryDo(async () =>
            {
                var request = Http.CreateRequest("GET", url);
                return await Http.Send(httpClient, request);
            })
        ).WithWarmUpDuration(duration: TimeSpan.FromSeconds(5))
        .WithLoadSimulations(Simulation.KeepConstant(100, during: TimeSpan.FromSeconds(5)));

        var runStats = NBomberRunner.RegisterScenarios(getHiStep)
            .Run();

        _outputHelper.WriteLine($"OK: {runStats.AllOkCount}, FAILED: {runStats.AllFailCount}, REQUESTS: {runStats.AllRequestCount}");

        runStats.AllOkCount.Should().BeGreaterThanOrEqualTo(duration * expectedRequestsPerSecond);
    }

    private async Task<IResponse> TryDo(Func<Task<Response<HttpResponseMessage>>> action)
    {
        try
        {
            return await action.Invoke();
        }
        catch 
        {
            return Response.Fail();
        }
    }
}
