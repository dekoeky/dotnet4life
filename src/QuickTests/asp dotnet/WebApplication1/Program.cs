using Microsoft.AspNetCore.MiddlewareAnalysis;
using Microsoft.AspNetCore.Mvc.Formatters;
using Scalar.AspNetCore;
using System.Diagnostics;
using WebApplication1.Configuration;
using WebApplication1.Diagnostics;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
}).AddJsonOptions(Json.ConfigureForController);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddApplicationServices();
builder.Services.AddApplicationHealthChecks();
builder.Services.ConfigureHttpJsonOptions(Json.ConfigureForMinimalApi);
//builder.Services.AddDbContext<MyDb>();
builder.Services.AddSingleton(Random.Shared);
builder.Services.AddSingleton<AsyncDataGenerator>();

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
app.MapApplicationHealthChecks();
app.MapControllers();
app.MapApplicationMinimalApis();

app.Run();

//Allows Unit Tests
namespace WebApplication1
{
    public partial class Program;
}
