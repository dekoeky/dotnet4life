namespace QuickTests.DependencyInjection.TestData;

public class MyDependency : CreatedAt, IMyDependency
{
    public static int InstancesCreated { get; private set; }

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