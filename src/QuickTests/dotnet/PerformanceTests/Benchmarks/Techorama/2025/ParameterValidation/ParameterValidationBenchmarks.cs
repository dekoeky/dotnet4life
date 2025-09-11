using BenchmarkDotNet.Attributes;
using SharedLibrary.Techorama._2025.ParameterValidation;

namespace PerformanceTests.Benchmarks.Techorama._2025.ParameterValidation;

[ShortRunJob]
[MemoryDiagnoser]
public class ParameterValidationBenchmarks
{
    private const int GoodParameter = 10;
    private const int BadParameter = -10;

    [Params(GoodParameter, BadParameter)]
    public int Parameter;

    [Benchmark]
    public int Incorrect()
    {
        var sum = 0;
        try
        {
            var enumerable = ParameterValidationImplementations.Incorrect(Parameter);

            foreach (var value in enumerable)
                sum += value;
        }
        catch
        {
            //Swallow the exception
        }

        return sum;
    }

    [Benchmark]
    public int Correct_Using_ExternalMethod()
    {
        var sum = 0;
        try
        {
            var enumerable = ParameterValidationImplementations.Correct_Using_ExternalMethod(Parameter);

            foreach (var value in enumerable)
                sum += value;
        }
        catch
        {
            //Swallow the exception
        }

        return sum;
    }

    [Benchmark]
    public int Correct_Using_LocalFunction()
    {
        var sum = 0;
        try
        {
            var enumerable = ParameterValidationImplementations.Correct_Using_LocalFunction(Parameter);

            foreach (var value in enumerable)
                sum += value;
        }
        catch
        {
            //Swallow the exception
        }

        return sum;
    }
}
