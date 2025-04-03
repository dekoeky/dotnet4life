using Microsoft.AspNetCore.MiddlewareAnalysis;
using Microsoft.AspNetCore.Mvc.Formatters;
using Scalar.AspNetCore;
using System.Diagnostics;
using WebApplication1.Configuration;
using WebApplication1.Diagnostics;

//// Add services to the container.

//builder.Services.AddOpenApi();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
    {
        options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
    })
    .AddJsonOptions(Json.Configure);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddApplicationServices();
builder.Services.AddApplicationHealthChecks();
builder.Services.ConfigureHttpJsonOptions(Json.Configure);

if (Features.MiddlewareDiagnostics)
    // insert the AnalysisStartupFilter as the first IStartupFilter in the container
    builder.Services.Insert(0, ServiceDescriptor.Transient<IStartupFilter, AnalysisStartupFilter>());

var app = builder.Build();

if (Features.MiddlewareDiagnostics)
{
    // Grab the "Microsoft.AspNetCore" DiagnosticListener from DI
    var listener = app.Services.GetRequiredService<DiagnosticListener>();

    // Create an instance of the AnalysisDiagnosticAdapter using the IServiceProvider
    // so that the ILogger is injected from DI
    var observer = ActivatorUtilities.CreateInstance<AnalysisDiagnosticAdapter>(app.Services);

    // Subscribe to the listener with the SubscribeWithAdapter() extension method
    using var disposable = listener.SubscribeWithAdapter(observer);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapApplicationHealthChecks();
app.MapApplicationMinimalApis();

app.Run();


//Allows Unit Tests
namespace WebApplication1
{
    public partial class Program;
}