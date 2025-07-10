using Microsoft.Extensions.DependencyInjection;
using QuickTests.DependencyInjection.TestData;
using System.Diagnostics;

namespace QuickTests.DependencyInjection;

[TestClass]
public class ServiceRegistrationTimingTests
{
    private readonly Action<ServiceCollection> _registerViaTypeParameter = s => s.AddSingleton<MyDependency>();
    private readonly Action<ServiceCollection> _registerViaFactory = s => s.AddSingleton<MyDependency>(sp => new MyDependency("Created During Registration"));
    private readonly Action<ServiceCollection> _registerInstance = s => s.AddSingleton(new MyDependency("Created During Registration"));


    [TestMethod] public void RegisterViaTypeParameter() => Test(_registerViaTypeParameter, ExpectedCreationTime.AfterRegistration);
    [TestMethod] public void RegisterViaFactory() => Test(_registerViaFactory, ExpectedCreationTime.AfterRegistration);
    [TestMethod] public void RegisterInstance() => Test(_registerInstance, ExpectedCreationTime.DuringRegistration);


    private static void Test(Action<ServiceCollection> registerServices, ExpectedCreationTime expectedCreationTime)
    {
        //Arrange
        var sc = new ServiceCollection();
        registerServices(sc);
        var registrationFinished = DateTime.Now;// Stopwatch.GetTimestamp();
        Debug.WriteLine($"[{registrationFinished:O}] Registration Finished");

        //Act
        using var sp = sc.BuildServiceProvider();
        var serviceProviderBuilt = DateTime.Now;// Stopwatch.GetTimestamp();
        Debug.WriteLine($"[{serviceProviderBuilt:O}] ServiceProvider Built");
        var service = sp.GetService<MyDependency>();

        //Assert
        Assert.IsNotNull(service);

        //Validate Service Creation Time
        switch (expectedCreationTime)
        {
            case ExpectedCreationTime.DuringRegistration:
                Assert.IsFalse(service.CreatedDateTime > registrationFinished, "The service was expected to be created during registration, but was created after registration");
                break;

            case ExpectedCreationTime.AfterRegistration:
                Assert.IsFalse(service.CreatedDateTime < registrationFinished, "The service was expected to be created after registration, but was created during registration");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(expectedCreationTime), expectedCreationTime, null);
        }
    }
}