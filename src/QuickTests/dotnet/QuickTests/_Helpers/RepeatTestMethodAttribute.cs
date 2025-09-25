namespace QuickTests._Helpers;

/// <summary>
/// Repeats the annotated test method <see cref="RepeatCount"/> times.
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class RepeatTestMethodAttribute : TestMethodAttribute
{
    private readonly int RepeatCount;

    public RepeatTestMethodAttribute(int repeatCount)
    {
        if (repeatCount < 1)
            throw new ArgumentOutOfRangeException(nameof(repeatCount), "Repeat count must be > 0");
        RepeatCount = repeatCount;
    }

    public override TestResult[] Execute(ITestMethod testMethod)
    {
        var results = new TestResult[RepeatCount];
        for (var i = 0; i < RepeatCount; i++)
        {
            var result = base.Execute(testMethod);
            results[i] = result[0];
            // you can annotate results[i].DisplayName here if you want to see run #N
            //results[i].DisplayName = $"{testMethod.TestClassName}.{testMethod.TestMethodName} (run {i + 1})";
        }
        return results;
    }
}