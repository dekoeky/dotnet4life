using Serilog;
using Shared.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.ClearProviders(); //To stop logging to console, via Microsoft providers
builder.Logging.AddSerilog(
    new LoggerConfiguration()
        .WriteTo.Console(outputTemplate: "A {Timestamp:O} [{Level:u3}] *°.{SourceContext}.°* {Message:lj}{NewLine}{Exception}")
        .WriteTo.Console(outputTemplate: "B {Timestamp:HH:mm:SS} [{Level:u3}] <=={SourceContext}==> {Message:lj}{NewLine}{Exception}")
        //.WriteTo.Logger(l =>
        //{

        //})
        //.WriteTo.File("Logs/Errors/log.txt", LogEventLevel.Error)
        //.WriteTo.File("Logs/Info/log.txt", LogEventLevel.Information)
        //.WriteTo.File("Logs/Debug/log.txt", LogEventLevel.Debug, outputTemplate: "{Timestamp:O} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
        .MinimumLevel.Debug()
        .CreateLogger()
    );

builder.Services.AddHostedService<Worker1>();
builder.Services.AddHostedService<Worker2>();

var host = builder.Build();
host.Run();

