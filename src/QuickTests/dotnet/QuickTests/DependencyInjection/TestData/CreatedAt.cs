using System.Diagnostics;

namespace QuickTests.DependencyInjection.TestData;

/// <summary>
/// Base class for services that track their creation time.
/// </summary>
public abstract class CreatedAt : ICreatedAt
{
    protected CreatedAt()
    {
        Debug.WriteLine($"[{CreatedDateTime:O}] {GetType().Name} constructor");
    }
    public long CreatedTimestamp { get; } = Stopwatch.GetTimestamp();
    public DateTime CreatedDateTime { get; } = DateTime.Now;
}