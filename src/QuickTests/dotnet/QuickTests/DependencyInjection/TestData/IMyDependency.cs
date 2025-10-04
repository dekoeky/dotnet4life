namespace QuickTests.DependencyInjection.TestData;

public interface IMyDependency : ICreatedAt
{
    string Content { get; }
}