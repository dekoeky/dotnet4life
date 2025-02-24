using Microsoft.AspNetCore.Mvc.Testing;
using System.Diagnostics;
using System.Net;
using Xunit.Abstractions;

namespace WebApplication1.Tests;

public class UnitTest1(WebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper) : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();

    [Fact]
    public async Task Test_SingleConcurrentRequest()
    {
        // Act
        var call1 = Test();   //This call will take 1s
        Thread.Sleep(10);           //Ensure the first request is first
        var call2 = Test();
        var result1 = await call1;
        var result2 = await call2;

        // Assert
        Assert.Equal(HttpStatusCode.OK, result1);
        Assert.Equal(HttpStatusCode.ServiceUnavailable, result2);
        return;

        async Task<HttpStatusCode> Test()
        {
            var sw = Stopwatch.StartNew();
            var response = await _client.GetAsync("Controllers/Test/SingleConcurrentRequest");

            var elapsed = sw.Elapsed;
            var statusCode = response.StatusCode;

            testOutputHelper.WriteLine($"[{(int)statusCode}] {statusCode} -> {elapsed}");
            return response.StatusCode;
        }
    }


    [Fact]
    public async Task Test_SlidingWindow()
    {
        // Act
        var request1 = Test();
        await Task.Delay(1500);
        var request2 = Test();
        await Task.Delay(1500);
        var request3 = Test();

        var result1 = await request1;
        var result2 = await request2;
        var result3 = await request3;

        // Assert
        Assert.Equal(HttpStatusCode.OK, result1);
        Assert.Equal(HttpStatusCode.ServiceUnavailable, result2);
        Assert.Equal(HttpStatusCode.OK, result3);
        return;

        async Task<HttpStatusCode> Test()
        {
            var sw = Stopwatch.StartNew();
            var response = await _client.GetAsync("Controllers/Test/SlidingWindow");

            var elapsed = sw.Elapsed;
            var statusCode = response.StatusCode;
            var header = response.Headers.TryGetValues("ReceivedAt", out var values) ? values.FirstOrDefault() : "???";

            testOutputHelper.WriteLine($"[{(int)statusCode}] {statusCode} -> {elapsed}, Received By Api @ {header}");
            return response.StatusCode;
        }
    }
}