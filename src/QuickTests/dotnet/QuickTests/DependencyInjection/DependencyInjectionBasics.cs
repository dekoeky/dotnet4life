using Microsoft.Extensions.DependencyInjection;
using QuickTests.DependencyInjection.TestData;

namespace QuickTests.DependencyInjection;

/// <summary>
/// Demonstrates basic usage of dependency injection in .NET.
/// </summary>
[TestClass]
public class DependencyInjectionBasics
{
    [TestMethod]
    public void SingletonServiceAsSelf()
    {
        // ---------- ARRANGE ----------
        var services = new ServiceCollection()
            .AddSingleton<MyDependency>();

        // ---------- ACT --------------
        using var serviceProvider = services.BuildServiceProvider();
        var first = serviceProvider.GetService<MyDependency>();
        var second = serviceProvider.GetService<MyDependency>();

        // ---------- ASSERT -----------
        Assert.IsNotNull(first, "Could not retrieve first service from service provider");
        Assert.IsNotNull(second, "Could not retrieve second service from service provider");
        Assert.IsInstanceOfType<MyDependency>(first, out var f);
        Assert.IsInstanceOfType<MyDependency>(second, out var s);
        Assert.AreSame(f, s, $"{nameof(first)} and {nameof(second)} are not the same reference, making the service NOT a singleton");
        Assert.AreEqual(1, MyDependency.InstancesCreated, $"More than one {nameof(MyDependency)} instance created");
        Assert.IsTrue(f.ParameterlessConstructorUsed, $"{nameof(first)} was created without the parameterless constructor");
        Assert.IsTrue(s.ParameterlessConstructorUsed, $"{nameof(first)} was created without the parameterless constructor");
    }

    [TestMethod]
    public void SingletonServiceAsInterface()
    {
        // ---------- ARRANGE ----------
        var services = new ServiceCollection()
            .AddSingleton<IMyDependency, MyDependency>();

        // ---------- ACT --------------
        using var serviceProvider = services.BuildServiceProvider();
        var first = serviceProvider.GetService<IMyDependency>();
        var second = serviceProvider.GetService<IMyDependency>();

        // ---------- ASSERT -----------
        Assert.IsNotNull(first, "Could not retrieve first service from service provider");
        Assert.IsNotNull(second, "Could not retrieve second service from service provider");
        Assert.IsInstanceOfType<MyDependency>(first, out var f);
        Assert.IsInstanceOfType<MyDependency>(second, out var s);
        Assert.AreSame(f, s, $"{nameof(first)} and {nameof(second)} are not the same reference, making the service NOT a singleton");
        Assert.AreEqual(1, MyDependency.InstancesCreated, $"More than one {nameof(MyDependency)} instance created");
        Assert.IsTrue(f.ParameterlessConstructorUsed, $"{nameof(first)} was created without the parameterless constructor");
        Assert.IsTrue(s.ParameterlessConstructorUsed, $"{nameof(first)} was created without the parameterless constructor");
    }

    [TestMethod]
    public void SingletonServiceAsSelfWithParameter()
    {
        // ---------- ARRANGE ----------
        var services = new ServiceCollection()
            .AddSingleton<MyDependency>(f => new MyDependency("Created using implementationFactory"));

        // ---------- ACT --------------
        using var serviceProvider = services.BuildServiceProvider();
        var first = serviceProvider.GetService<MyDependency>();
        var second = serviceProvider.GetService<MyDependency>();

        // ---------- ASSERT -----------
        Assert.IsNotNull(first, "Could not retrieve first service from service provider");
        Assert.IsNotNull(second, "Could not retrieve second service from service provider");
        Assert.IsInstanceOfType<MyDependency>(first, out var f);
        Assert.IsInstanceOfType<MyDependency>(second, out var s);
        Assert.AreSame(f, s, $"{nameof(first)} and {nameof(second)} are not the same reference, making the service NOT a singleton");
        Assert.AreEqual(1, MyDependency.InstancesCreated, $"More than one {nameof(MyDependency)} instance created");
        Assert.IsTrue(f.ParameterlessConstructorUsed, $"{nameof(first)} was created without the parameterless constructor");
        Assert.IsTrue(s.ParameterlessConstructorUsed, $"{nameof(first)} was created without the parameterless constructor");
    }
}