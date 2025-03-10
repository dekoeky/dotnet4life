using BenchmarkDotNet.Attributes;

namespace PerformanceTests.Attributes;

[AttributeUsage(AttributeTargets.Class)]
internal class BenchmarkCategoryFromNamespaceAttribute(Type benchmarkType)
    : BenchmarkCategoryAttribute(benchmarkType.Namespace ?? throw new ArgumentNullException(nameof(benchmarkType)));