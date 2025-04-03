using ClassLibrary1.Clients;
using WorkerService1;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddTimeApiClient(builder.Configuration);
builder.Services.AddHostedService<TimeOfDayApiConsumerService>();

var host = builder.Build();
host.Run();
