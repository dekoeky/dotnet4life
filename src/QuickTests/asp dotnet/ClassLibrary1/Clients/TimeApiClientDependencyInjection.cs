using ClassLibrary1.HttpHandlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ClassLibrary1.Clients;

public static class TimeApiClientDependencyInjection
{
    public static IHostApplicationBuilder AddTimeApiClient(this IHostApplicationBuilder builder)
    {
        builder.Services.AddTimeApiClient(builder.Configuration);

        return builder;
    }

    public static void AddTimeApiClient(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHttpContextAccessor()
            .AddTransient<TimeOfSendingHeaderHandler>()
            .AddHttpClient<ITimeApiClient, TimeApiClient>(client =>
            {
                client.BaseAddress = configuration.GetValue<Uri>("TimeApi:Url");
            })
            .AddHttpMessageHandler<TimeOfSendingHeaderHandler>();
    }
}