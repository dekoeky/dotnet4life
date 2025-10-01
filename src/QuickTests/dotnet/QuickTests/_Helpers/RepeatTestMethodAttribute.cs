namespace QuickTests._Helpers;

/// <summary>
/// Repeats the annotated test method <see cref="_repeatCount"/> times.
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public sealed class RepeatTestMethodAttribute : TestMethodAttribute
{
    private readonly int _repeatCount;

    public RepeatTestMethodAttribute(int repeatCount)
    {
        if (repeatCount < 1)
            throw new ArgumentOutOfRangeException(nameof(repeatCount), "Repeat count must be > 0");
        _repeatCount = repeatCount;
    }

    public override TestResult[] Execute(ITestMethod testMethod)
    {
        var results = new TestResult[_repeatCount];
        for (var i = 0; i < _repeatCount; i++)
        {
            var result = base.Execute(testMethod);
            results[i] = result[0];
            // you can annotate results[i].DisplayName here if you want to see run #N
            //results[i].DisplayName = $"{testMethod.TestClassName}.{testMethod.TestMethodName} (run {i + 1})";
        }
        return results;
    }
}