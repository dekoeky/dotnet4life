using Microsoft.Extensions.DependencyInjection;
using QuickTests.DependencyInjection.TestData;

namespace QuickTests.DependencyInjection.KeyedServices;

[TestClass]
public class KeyedServicesBasicsTests
{
    /// <summary>
    /// Demonstrates the <see cref="FromKeyedServicesAttribute"/>.
    /// </summary>
    [TestMethod]
    public void FromKeyedServices()
    {
        //Arrange
        var sc = new ServiceCollection()
            .AddKeyedSingleton<IMyDependency>("A", new MyDependency("A"))
            .AddKeyedSingleton<IMyDependency>("B", new MyDependency("B"))
            .AddSingleton<MyService.A>()  //See constructors for more context
            .AddSingleton<MyService.B>();
        using var sp = sc.BuildServiceProvider();

        //Act
        var a = sp.GetService<MyService.A>();
        var b = sp.GetService<MyService.B>();
        var contentA = a?.GetDependencyContent();
        var contentB = b?.GetDependencyContent();

        //Assert
        Assert.IsNotNull(a);
        Assert.IsNotNull(b);
        Assert.AreEqual("A", contentA);
        Assert.AreEqual("B", contentB);
    }

    /// <summary>
    /// Demonstrates that you can ONLY retrieve keyed services with the key of the correct type.
    /// </summary>
    [TestMethod]
    public void CanOnlyRetrieveWhenKeyIsCorrectType()
    {
        //Arrange
        const byte key = 15;
        var sc = new ServiceCollection()
            .AddKeyedSingleton<IMyDependency>(key, new MyDependency("Hello World"));
        using var sp = sc.BuildServiceProvider();

        //Act
        Assert.IsNotNull(sp.GetKeyedService<IMyDependency>(key));

        //The following references are not equal to the original key
        Assert.IsNull(sp.GetKeyedService<IMyDependency>((short)key));
        Assert.IsNull(sp.GetKeyedService<IMyDependency>((int)key));
        Assert.IsNull(sp.GetKeyedService<IMyDependency>((long)key));
        Assert.IsNull(sp.GetKeyedService<IMyDependency>((ushort)key));
        Assert.IsNull(sp.GetKeyedService<IMyDependency>((uint)key));
        Assert.IsNull(sp.GetKeyedService<IMyDependency>((ulong)key));
    }

    /// <summary>
    /// Demonstrates that you cannot retrieve (all) keyed services using the GetService or GetServices methods.
    /// </summary>
    [TestMethod]
    public void KeyedServicesDoNotReturnForGetServices()
    {
        //Arrange
        var sc = new ServiceCollection()
            .AddKeyedSingleton<IMyDependency>("A", new MyDependency("A"))
            .AddKeyedSingleton<IMyDependency>("B", new MyDependency("B"))
            .AddKeyedSingleton<IMyDependency>("C", new MyDependency("B"));
        using var sp = sc.BuildServiceProvider();

        //Act
        var countGetServices = sp.GetServices<IMyDependency>().Count();
        var countGetServiceIEnumerable = sp.GetService<IEnumerable<IMyDependency>>()?.Count();

        //Assert
        Assert.AreEqual(0, countGetServices);
        Assert.AreEqual(0, countGetServiceIEnumerable);
    }
}