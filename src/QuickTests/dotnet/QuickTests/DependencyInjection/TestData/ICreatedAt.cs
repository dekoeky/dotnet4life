namespace QuickTests.DependencyInjection.TestData;

public interface ICreatedAt
{
    /// <summary>
    /// The (high accuracy) timestamp (typically obtained from <see cref="System.Diagnostics.Stopwatch.GetTimestamp"/>) at which the service was created.
    /// </summary>
    long CreatedTimestamp { get; }

    /// <summary>
    /// The (low accuracy) timestamp (typically obtained from <see cref="DateTime.Now"/>) at which the service was created.
    /// </summary>
    DateTime CreatedDateTime { get; }
}