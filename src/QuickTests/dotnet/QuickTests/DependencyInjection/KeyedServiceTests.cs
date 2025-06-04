using Microsoft.Extensions.DependencyInjection;

namespace QuickTests.DependencyInjection;

[TestClass]
public class KeyedServiceTests
{
    [TestMethod]
    public void Test1()
    {
        //Arrange
        var services = new ServiceCollection();
        services.AddSingleton<IData>(new Data("Hello World"));
        using var serviceProvider = services.BuildServiceProvider();

        //Act
        var service = serviceProvider.GetService<IData>();

        //Assert
        Assert.IsNotNull(service);
    }

    [TestMethod]
    public void Test2()
    {
        //Arrange
        int[] keys = [1, 2, 3, 4, 5];
        var services = new ServiceCollection();
        foreach (var key in keys)
            services.AddKeyedSingleton<IData>(key, new Data($"Content{key}"));
        using var serviceProvider = services.BuildServiceProvider();

        foreach (var key in keys)
        {
            //Act
            var service = serviceProvider.GetKeyedService<IData>(key);

            //Assert
            Assert.IsNotNull(service);
            Console.WriteLine($"Succesfully Retrieved Service with Key '{key}': {service.Content}");
        }
    }

    [TestMethod]
    public void Test3()
    {
        //Arrange
        var services = new ServiceCollection();
        services.AddKeyedSingleton<IData>("A", new Data("A"));
        services.AddKeyedSingleton<IData>("B", new Data("B"));
        services.AddSingleton<ServiceA>();
        services.AddSingleton<ServiceB>();
        using var serviceProvider = services.BuildServiceProvider();

        //Act
        var a = serviceProvider.GetService<ServiceA>();
        var b = serviceProvider.GetService<ServiceB>();
        var contentA = a?.GetContent();
        var contentB = b?.GetContent();

        //Assert
        Assert.IsNotNull(a);
        Assert.IsNotNull(b);
        Assert.AreEqual("A", contentA);
        Assert.AreEqual("B", contentB);
    }

    [TestMethod]
    public void Test4()
    {
        //Arrange
        byte key = 15;
        var services = new ServiceCollection();
        services.AddKeyedSingleton<IData>(key, new Data("Hello World"));
        using var serviceProvider = services.BuildServiceProvider();

        //Act
        Assert.IsNotNull(serviceProvider.GetKeyedService<IData>(key));

        //The following references are not equal to the original key
        Assert.IsNull(serviceProvider.GetKeyedService<IData>((short)key));
        Assert.IsNull(serviceProvider.GetKeyedService<IData>((int)key));
        Assert.IsNull(serviceProvider.GetKeyedService<IData>((long)key));
        Assert.IsNull(serviceProvider.GetKeyedService<IData>((ushort)key));
        Assert.IsNull(serviceProvider.GetKeyedService<IData>((uint)key));
        Assert.IsNull(serviceProvider.GetKeyedService<IData>((ulong)key));
    }

    private class ServiceA([FromKeyedServices("A")] IData data)
    {
        public string GetContent() => data.Content;
    }
    private class ServiceB([FromKeyedServices("B")] IData data)
    {
        public string GetContent() => data.Content;
    }
    private record Data(string Content) : IData;

    private interface IData
    {
        public string Content { get; }
    }
}