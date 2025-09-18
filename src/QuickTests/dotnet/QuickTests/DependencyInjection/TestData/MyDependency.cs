namespace QuickTests.DependencyInjection.TestData;

public class MyDependency : CreatedAt, IMyDependency
{
    public static int InstancesCreated { get; private set; }

    /// <summary>
    /// Used to reset the static <see cref="InstancesCreated"/> before a second test.
    /// </summary>
    public static void ResetInstancesCreated() => InstancesCreated = 0;

    public MyDependency() : this("Default content")
    {
        ParameterlessConstructorUsed = true;
    }

    public MyDependency(string content)
    {
        InstanceId = ++InstancesCreated;
        Content = content;
    }

    public string Content { get; init; }

    public int InstanceId { get; }
    public bool ParameterlessConstructorUsed { get; }
}