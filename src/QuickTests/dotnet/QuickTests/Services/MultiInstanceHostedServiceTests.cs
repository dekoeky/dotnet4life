using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace QuickTests.Services;

[TestClass]
public class MultiInstanceHostedServiceTests
{
    [TestMethod]
    public void Test()
    {

        var host = Host.CreateApplicationBuilder();

        //No Talkie please
        host.Logging.ClearProviders();

        ////Does not work for multiple instances:
        //host.Services.AddHostedService<Incrementer>();
        //host.Services.AddHostedService<Incrementer>();

        //Works:
        host.Services.AddSingleton<IHostedService, Incrementer>();
        host.Services.AddSingleton<IHostedService, Incrementer>();
        host.Services.AddSingleton<IHostedService, Incrementer>();
        host.Services.AddSingleton<IHostedService, Incrementer>();
        host.Services.AddSingleton<IHostedService, Incrementer>();

        ////Hangs the app:
        //host.Services.AddKeyedSingleton<IHostedService, Incrementer>(1);
        //host.Services.AddKeyedSingleton<IHostedService, Incrementer>(2);
        //host.Services.AddKeyedSingleton<IHostedService, Incrementer>(3);
        //host.Services.AddKeyedSingleton<IHostedService, Incrementer>(4);
        //host.Services.AddKeyedSingleton<IHostedService, Incrementer>(5);

        var app = host.Build();
        app.Run();

        Assert.AreEqual(5, Incrementer.StartCount);
    }


    private class Incrementer(IHostApplicationLifetime lifetime) : BackgroundService
    {
        public static int StartCount;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Interlocked.Increment(ref StartCount);

            //Give other instances some time to run
            await Task.Delay(1000, stoppingToken);

            //Stop app
            lifetime.StopApplication();
        }
    }
}
