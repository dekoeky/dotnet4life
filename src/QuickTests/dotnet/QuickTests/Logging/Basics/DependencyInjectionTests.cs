using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace QuickTests.Logging.Basics;

[TestClass]
public class DependencyInjectionTests
{
    [TestMethod]
    public void SimpleDependencyInjection()
    {
        // ---------- ARRANGE ----------
        using var sp = new ServiceCollection()
            .AddLogging(configure => configure.AddConsole())
            .BuildServiceProvider();

        // ---------- ACT --------------
        //Remark: DI depends on the type parameter to determine the category name of the logger.
        var loggerWithCategory = sp.GetRequiredService<ILogger<DependencyInjectionTests>>();

        // ---------- ASSERT -----------
        loggerWithCategory.LogInformation("Hello World!");
    }

    /// <summary>
    /// This example demonstrates how user code, depending on <see cref="ILogger{T}"/> (or <see cref="ILogger"/>) can still work,
    /// even if no logging providers are configured. In this case, no logs will be emitted anywhere. 
    /// </summary>
    [TestMethod]
    public void NoLoggerProviders()
    {
        // ---------- ARRANGE ----------
        using var sp = new ServiceCollection()
            .AddLogging()                       //No providers configured. AddLogging() is still needed to register ILogger<T>.
            .BuildServiceProvider();

        // ---------- ACT --------------
        var logger = sp.GetRequiredService<ILogger<DependencyInjectionTests>>();
        logger.LogInformation("Hello World!");
    }
}