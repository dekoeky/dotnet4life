namespace QuickTests.DependencyInjection.TestData;

public interface IMyDependency : ICreatedAt
{
    public string Content { get; }
}