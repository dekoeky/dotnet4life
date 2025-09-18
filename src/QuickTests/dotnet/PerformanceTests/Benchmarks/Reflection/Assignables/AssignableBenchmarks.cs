using BenchmarkDotNet.Attributes;
using SharedLibrary.Reflection.Assignables;
using System.Diagnostics.CodeAnalysis;

namespace PerformanceTests.Benchmarks.Reflection.Assignables;

[ShortRunJob]
[SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Cleaner result Summary Table")]
public class AssignableBenchmarks
{
    // * Summary *

    //BenchmarkDotNet v0.15.3, Windows 11 (10.0.22631.5771/23H2/2023Update/SunValley3)
    //13th Gen Intel Core i9-13950HX 2.20GHz, 1 CPU, 32 logical and 24 physical cores
    //.NET SDK 10.0.100-rc.1.25451.107
    //  [Host]   : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3[AttachedDebugger]
    //  ShortRun : .NET 9.0.9 (9.0.9, 9.0.925.41916), X64 RyuJIT x86-64-v3

    //Job = ShortRun  IterationCount=3  LaunchCount=1
    //WarmupCount=3

    //| Method           | Type    | BaseType | Assignable | Mean     | Error     | StdDev    |
    //|----------------- |-------- |--------- |----------- |---------:|----------:|----------:|
    //| IsAssignableTo   | Animal  | Cat      | False      | 4.503 ns | 0.7722 ns | 0.0423 ns |
    //| IsAssignableFrom | Animal  | Cat      | False      | 4.300 ns | 4.7559 ns | 0.2607 ns |
    //| IsAssignableTo   | Animal  | Dog      | False      | 4.395 ns | 0.8411 ns | 0.0461 ns |
    //| IsAssignableFrom | Animal  | Dog      | False      | 4.265 ns | 1.3771 ns | 0.0755 ns |
    //| IsAssignableTo   | Animal  | IAnimal  | True       | 4.696 ns | 3.1273 ns | 0.1714 ns |
    //| IsAssignableFrom | Animal  | IAnimal  | True       | 4.178 ns | 1.3670 ns | 0.0749 ns |
    //| IsAssignableTo   | Cat     | Animal   | True       | 4.475 ns | 2.4012 ns | 0.1316 ns |
    //| IsAssignableFrom | Cat     | Animal   | True       | 4.208 ns | 1.2624 ns | 0.0692 ns |
    //| IsAssignableTo   | Cat     | Dog      | False      | 4.791 ns | 6.5006 ns | 0.3563 ns |
    //| IsAssignableFrom | Cat     | Dog      | False      | 4.179 ns | 2.4614 ns | 0.1349 ns |
    //| IsAssignableTo   | Cat     | IAnimal  | True       | 4.425 ns | 1.9065 ns | 0.1045 ns |
    //| IsAssignableFrom | Cat     | IAnimal  | True       | 4.619 ns | 6.7453 ns | 0.3697 ns |
    //| IsAssignableTo   | Dog     | Animal   | True       | 4.600 ns | 1.7244 ns | 0.0945 ns |
    //| IsAssignableFrom | Dog     | Animal   | True       | 4.366 ns | 4.8568 ns | 0.2662 ns |
    //| IsAssignableTo   | Dog     | Cat      | False      | 4.838 ns | 7.2866 ns | 0.3994 ns |
    //| IsAssignableFrom | Dog     | Cat      | False      | 4.258 ns | 1.6474 ns | 0.0903 ns |
    //| IsAssignableTo   | Dog     | IAnimal  | True       | 4.659 ns | 1.5758 ns | 0.0864 ns |
    //| IsAssignableFrom | Dog     | IAnimal  | True       | 4.153 ns | 2.0277 ns | 0.1111 ns |
    //| IsAssignableTo   | IAnimal | Animal   | False      | 4.678 ns | 1.0052 ns | 0.0551 ns |
    //| IsAssignableFrom | IAnimal | Animal   | False      | 4.076 ns | 0.8772 ns | 0.0481 ns |
    //| IsAssignableTo   | IAnimal | Cat      | False      | 4.486 ns | 4.7439 ns | 0.2600 ns |
    //| IsAssignableFrom | IAnimal | Cat      | False      | 4.184 ns | 4.2162 ns | 0.2311 ns |
    //| IsAssignableTo   | IAnimal | Dog      | False      | 4.627 ns | 4.7625 ns | 0.2610 ns |
    //| IsAssignableFrom | IAnimal | Dog      | False      | 4.041 ns | 1.1458 ns | 0.0628 ns |




    //Remark: For some reason (when directly using a test data method form an external class) using the following attribute does not work.
    // Does not work: [ArgumentsSource(typeof(AssignablesTestData), nameof(AssignablesTestData.AsObjects))]
    //Workaround, create a local test data method, that points to the actual testdata.
    public static IEnumerable<object[]> Cases() => AssignablesTestData
        .Get()
        .Select(t => new object[]
        {
            //By Wrapping the types in this type, we control the formatting of the columns in the results
            new TypeWrapper(t.Type),
            new TypeWrapper(t.BaseType),
            t.Assignable
        });


    [Benchmark]
    [ArgumentsSource(nameof(Cases))]
    public bool IsAssignableTo(TypeWrapper Type, TypeWrapper BaseType, bool Assignable) =>
        Type.Type.IsAssignableTo(BaseType.Type);

    [Benchmark]
    [ArgumentsSource(nameof(Cases))]
    public bool IsAssignableFrom(TypeWrapper Type, TypeWrapper BaseType, bool Assignable) =>
        BaseType.Type.IsAssignableFrom(Type.Type);

    /// <summary>
    /// Created to prevent long namespace causing the type to be unreadable in the results
    /// </summary>
    /// <param name="type"></param>
    public class TypeWrapper(Type type)
    {
        public Type Type { get; } = type;
        public override string ToString() => Type.Name; //Show the short name
    }
}