using Serilog;

Console.WriteLine("Hello, World!");


using var logger = new LoggerConfiguration()
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss.fffffff}][{Level:u3}] ({SourceContext}) {Message:lj}{NewLine}{Exception}")
    .CreateLogger();



var ferrariLogger = logger.ForContext<Mercedes>();
var fiatLogger = logger.ForContext<Renault>();

ferrariLogger.Information("Vroom Vroom");
fiatLogger.Information("Tuut tuut");


file class Mercedes;
file class Renault;