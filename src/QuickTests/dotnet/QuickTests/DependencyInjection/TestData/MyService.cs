using Microsoft.Extensions.DependencyInjection;

namespace QuickTests.DependencyInjection.TestData;

/// <summary>
/// A services that depends on a <see cref="IMyDependency"/> instance.
/// </summary>
public class MyService(IMyDependency myDependency) : CreatedAt
{
    public string GetDependencyContent() => myDependency.Content;

    public DateTime CreatedAt { get; } = DateTime.Now;

    //Two different implementations of MyService, each with a different key for the dependency.
    /// <summary>
    /// An implementation of <see cref="MyService"/> that requires an <see cref="IMyDependency"/> instance with the key "A".
    /// </summary>
    public class A([FromKeyedServices("A")] IMyDependency myDependency) : MyService(myDependency);
    /// <summary>
    /// An implementation of <see cref="MyService"/> that requires an <see cref="IMyDependency"/> instance with the key "B".
    /// </summary>
    public class B([FromKeyedServices("B")] IMyDependency myDependency) : MyService(myDependency);
}