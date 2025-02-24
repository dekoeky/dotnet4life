using System.Numerics;

namespace xUnit.Tests;

public class MyClassTests
{
    private T GetValue<T>(T input) where T : INumber<T>
    {
        return input;
    }

    // Externalized test data
    public static IEnumerable<object[]> TestData =>
        new List<object[]>
        {
            new object[] { 42 },
            new object[] { 3.14 },
            new object[] { "hello" },
            new object[] { true }
        };

    [Theory]
    [MemberData(nameof(TestData))]
    public void Test_GenericMethod_WithVariousValues<T>(T value) where T : INumber<T>
    {
        var result = GetValue(value);

        // Assert that the result is the same as the input value
        Assert.Equal(value, result);
    }
}