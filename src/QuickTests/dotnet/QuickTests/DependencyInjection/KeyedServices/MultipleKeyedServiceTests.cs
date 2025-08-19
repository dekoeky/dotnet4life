using Microsoft.Extensions.DependencyInjection;
using QuickTests.DependencyInjection.TestData;
using System.Diagnostics;

namespace QuickTests.DependencyInjection.KeyedServices;

[TestClass]
public class MultipleKeyedServiceTests
{
    private readonly int[] _keys = [1, 2, 3, 4, 5];

    private readonly Action<ServiceCollection, int> _registerViaTypeParameter =
        (sc, key) => sc.AddKeyedSingleton<MyDependency>(key);

    private readonly Action<ServiceCollection, int> _registerInstancesAtRegistrationTime = (sc, key) =>
        sc.AddKeyedSingleton(key, new MyDependency($"[{key}] Created At Registration around {DateTime.Now}"));

    private readonly Action<ServiceCollection, int> _registerViaFactoryMethod = (sc, key) =>
        sc.AddKeyedSingleton(key, (_, k) =>
        {
            var d = new MyDependency($"{key}");
            Debug.WriteLine($"[{DateTime.Now:O}] Created {nameof(IMyDependency)} service with key {key}");
            return d;
        });


    [TestMethod]
    public void RegisterViaTypeParameter() => Test(_registerViaTypeParameter);

    [TestMethod]
    public void RegisterViaFactoryMethod() => Test(_registerViaFactoryMethod);

    [TestMethod]
    public void RegisterViaRegistrationTimeCreatedInstances() => Test(_registerInstancesAtRegistrationTime);

    private void Test(Action<ServiceCollection, int> registerOneKeyedService)
    {
        // ---------- ARRANGE ----------
        var sc = new ServiceCollection();

        //For each key, register a keyed singleton service (with the same key dependant factory method)
        foreach (var key in _keys)
            registerOneKeyedService(sc, key);

        using var sp = sc.BuildServiceProvider();

        foreach (var key in _keys)
        {
            // ---------- ACT --------------
            Debug.WriteLine(string.Empty);
            Debug.WriteLine($"[{DateTime.Now:O}] Retrieving {nameof(IMyDependency)}({key})...");
            var service = sp.GetKeyedService<MyDependency>(key);

            // ---------- ASSERT -----------
            Assert.IsNotNull(service);
            Debug.WriteLine($"[{DateTime.Now:O}] Successfully Retrieved Service with Key '{key}': {service.Content}");
        }
    }
}