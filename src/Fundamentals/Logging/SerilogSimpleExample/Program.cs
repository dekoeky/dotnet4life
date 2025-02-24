// See https://aka.ms/new-console-template for more information

using Serilog;

Console.WriteLine("Hello, World!");


using var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();