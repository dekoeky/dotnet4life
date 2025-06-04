using Microsoft.Extensions.DependencyInjection;

namespace QuickTests.DependencyInjection;

[TestClass]
public class KeyedServiceTests
{
    [TestMethod]
    public void Test()
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

    private record Data(string Content) : IData;

    private interface IData
    {
        public string Content { get; }
    }
}