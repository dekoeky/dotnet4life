using RequestLocalization;
using RequestLocalization.RequestCultureProviders;
using RequestLocalization.Services.Development;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers(options =>
{
    //options.Conventions.Add(new CultureRouteConvention());
});
builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});
builder.Services.AddRequestLocalization(o =>
{
    o.ApplyCurrentCultureToResponseHeaders = true;
    //o.RequestCultureProviders = //Default
    //o.RequestCultureProviders.Add(new ExtractRequestCultureFromRoute());
    o.RequestCultureProviders.Insert(0, new RandomRequestCultureProvider());
    //o.RequestCultureProviders.Add(new RouteDataRequestCultureProvider());

    switch (builder.Configuration["YOLO"])
    {
        case "Random":
            o.RequestCultureProviders = [new RandomRequestCultureProvider()];
            break;


        default:


    }

    o.SetDefaultCulture(CultureSupport.DefaultCulture)
        .AddSupportedCultures(CultureSupport.SupportedCultures)
        .AddSupportedUICultures(CultureSupport.SupportedCultures);
});
//builder.Services.Configure<RequestLocalizationOptions>()
builder.Services.AddHostedService<RequestLocalizationOptionsPrinterService>();

var app = builder.Build();

app.UseRequestLocalization(useDebugger: true);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
