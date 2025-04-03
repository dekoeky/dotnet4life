using Formatters.Endpoints;
using Formatters.Formatters.Output;
using Formatters.Services.Demo;
using Formatters.Services.WeatherForecasts;
using Microsoft.AspNetCore.Mvc.Formatters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{

    options.InputFormatters.Add(new XmlSerializerInputFormatter(options));

    options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
    options.OutputFormatters.Add(new SimpleCsvOutputFormatter());

    return;
    // F12 shenanigans:
    options.OutputFormatters.Add(new StringOutputFormatter());
    options.OutputFormatters.Add(new HttpNoContentOutputFormatter());
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<MvcFormattersPrinterService>();
builder.Services.AddSingleton<IWeatherForecastService, WeatherForecastService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapMinimalApis();

app.Run();