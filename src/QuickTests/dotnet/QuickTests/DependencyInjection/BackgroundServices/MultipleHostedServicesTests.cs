using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuickTests.DependencyInjection.BackgroundServices.TestData;

namespace QuickTests.DependencyInjection.BackgroundServices;

[TestClass]
public class MultipleHostedServicesTests
{

    [TestMethod]
    public async Task One_AddHostedService() => await Test(
        builder =>
        {
            builder.Services.AddHostedService<MyBackgroundService>();
        },
        () =>
        {
            Assert.AreEqual(1, MyBackgroundService.GetRunCount(0));
        });

    [TestMethod]
    public async Task Two_AddHostedService_DoesNotWork()
    {
        const int willRun = 1;
        const int willNotRun = 2;

        await Test(
            builder =>
            {
                builder.Services.AddHostedService<MyBackgroundService>(_ => new MyBackgroundService(willRun));
                builder.Services.AddHostedService<MyBackgroundService>(_ => new MyBackgroundService(willNotRun)); // This one is ignored
            },
            () =>
            {
                Assert.AreEqual(1, MyBackgroundService.GetRunCount(willRun));
                Assert.AreEqual(0, MyBackgroundService.GetRunCount(willNotRun));
            });
    }

    [TestMethod]
    public async Task Two_AddHostedServiceSingleTon_Works()
    {
        await Test(
            builder =>
            {
                builder.Services.AddSingleton<IHostedService>(_ => new MyBackgroundService(1));
                builder.Services.AddSingleton<IHostedService>(_ => new MyBackgroundService(2));
            },
            () =>
            {
                Assert.AreEqual(1, MyBackgroundService.GetRunCount(1));
                Assert.AreEqual(1, MyBackgroundService.GetRunCount(2));
            });
    }

    [TestMethod]
    public async Task Two_AddHostedServiceSingleTon_Works2()
    {
        await Test(
            builder =>
            {
                builder.Services.AddSingleton<IHostedService, MyBackgroundService>();
                builder.Services.AddSingleton<IHostedService, MyBackgroundService>();
            },
            () =>
            {
                Assert.AreEqual(2, MyBackgroundService.GetRunCount(MyBackgroundService.DefaultInstance));
            });
    }


    [TestInitialize]
    public void Initialize() => MyBackgroundService.ResetRunCounts();

    private static async Task Test(Action<HostApplicationBuilder> configureServices, Action assert)
    {
        // ---------- ARRANGE ----------
        var builder = Host.CreateEmptyApplicationBuilder(new HostApplicationBuilderSettings()
        {
            ApplicationName = "Unit Test",
            EnvironmentName = "UnitTest",
        });
        configureServices(builder);
        using var host = builder.Build();

        // ---------- ACT --------------
        _ = host.StartAsync();
        await Task.Delay(100);

        // ---------- ASSERT -----------
        foreach (var (instance, runCount) in MyBackgroundService.Data) Console.WriteLine($"[{instance}] RunCount: {runCount}");
        assert();

        // --------- CLEANUP -----------
        await host.StopAsync();
    }
}