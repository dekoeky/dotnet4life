using BenchmarkDotNet.Attributes;

namespace PerformanceTests.Benchmarks.DataTypes.Strings.Formatting;

[MemoryDiagnoser]
public class SimpleStringFormattingBenchmarksWithManyParameters
{
    //TODO: Use formatting expressions

    private readonly Parameters _parameters = new();

    [Benchmark]
    public string StringConcatenation() =>
        _parameters.P01 + " / " +
        _parameters.P02 + " / " +
        _parameters.P03 + " / " +
        _parameters.P04 + " / " +
        _parameters.P05 + " / " +
        _parameters.P06 + " / " +
        _parameters.P07;

    [Benchmark]
    public string StringJoin() => string.Join(" / ",
        _parameters.P01,
        _parameters.P02,
        _parameters.P03,
        _parameters.P04,
        _parameters.P05,
        _parameters.P06,
        _parameters.P07);

    [Benchmark]
    // ReSharper disable once UseStringInterpolation
    public string StringFormat() => string.Format("{0} / {1} / {2} / {3} / {4} / {5} / {6}",
        _parameters.P01,
        _parameters.P02,
        _parameters.P03,
        _parameters.P04,
        _parameters.P05,
        _parameters.P06,
        _parameters.P07);

    [Benchmark]
    public string StringInterpolation() => $"{_parameters.P01} / {_parameters.P02} / {_parameters.P03} / {_parameters.P04} / {_parameters.P05} / {_parameters.P06} / {_parameters.P07}";


    private class Parameters
    {
        public DateTime P01 { get; } = DateTime.Now;
        public string P02 { get; } = "RendererName";
        public int P03 { get; } = int.MaxValue;
        public double P04 { get; } = -Math.PI;
        public float P05 { get; } = MathF.PI;
        public Guid P06 { get; } = Guid.Parse("BB281CB4-1BB7-46F2-84C0-27351F5D5B7E");
        public bool P07 { get; } = true;
    }
}