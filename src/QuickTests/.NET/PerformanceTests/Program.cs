using BenchmarkDotNet.Running;
using System.Reflection;

var summary = new BenchmarkSwitcher(Assembly.GetExecutingAssembly()).Run(args);