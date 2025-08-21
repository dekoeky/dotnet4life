using Microsoft.Extensions.Configuration.CommandLine;

namespace WorkerService1;

public class Worker(ILogger<Worker> logger, IHostEnvironment environment, IConfiguration configuration, IHostApplicationLifetime applicationLifetime) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var dotnetEnvironmentVariable = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
        var dotnetEnvironmentConfig = configuration["DOTNET_ENVIRONMENT"] ?? "(null)";
        var message = configuration["Message"] ?? "(null)";
        string cliProvidedValues = "0";
        var winningProvider = "???";

        if (configuration is IConfigurationRoot configurationRoot)
        {
            ////For Debug:
            //Console.WriteLine(rootConfig.GetDebugView());

            var cli = GetConfigurationsFromCli(configurationRoot);
            if (cli.Count > 0)
                cliProvidedValues = $"""
                           {cli.Count}
                           {string.Join(Environment.NewLine, cli.Select(kv => $"    '{kv.Key}'=\"{kv.Value}\""))}
                           """;

            //PrintEnvironmentNameFromEachProvider( rootConfig);
            winningProvider = GetWinningProvider(configurationRoot);
        }

        logger.LogInformation("""
                              ##############################################################
                              Application Name:   {ApplicationName}
                              Environment Name:   {EnvironmentName} ({WinningProvider})
                              DOTNET_ENVIRONMENT: {DOTNET_ENVIRONMENT}
                              CLI Provided Configuration Values:          {CliConfigArguments}
                              CommandLineArgs:    {CommandLineArgs}
                              Message:            {Message}
                              ##############################################################
                              """,
            environment.ApplicationName,
            environment.EnvironmentName,
            winningProvider,
            dotnetEnvironmentConfig,
            cliProvidedValues,
            Environment.GetCommandLineArgs(),
            message
        );

        if (configuration.GetValue("FastExit", false))
            applicationLifetime.StopApplication();
        else
            Console.WriteLine("Press Ctrl+C to exit application...");
    }

    private static Dictionary<string, string?> GetConfigurationsFromCli(IConfigurationRoot configurationRoot)
    {
        //Assume we always have 1 CommandLineConfigurationProvider
        if (configurationRoot.Providers.OfType<CommandLineConfigurationProvider>().FirstOrDefault() is not { } provider)
            return [];

        Dictionary<string, string?> results = [];

        foreach (var key in provider.GetChildKeys([], null))
            results[key] = provider.TryGet(key, out var value)
                ? value
                : null;

        return results;
    }

    private static void PrintEnvironmentNameFromEachProvider(IConfigurationRoot rootConfig)
    {
        Console.WriteLine("-----------------------------------------------");
        foreach (var provider in (rootConfig).Providers)
        {
            if (TryGetEnvironmentName(provider, out var value))
                Console.WriteLine($"{provider} provided environment = {value}");
            //else
            //    Console.WriteLine($"{provider} did not provide!");

        }
        Console.WriteLine("-----------------------------------------------");
    }

    /// <summary>
    /// Which is the provider that provided the final environment name?
    /// </summary>
    /// <param name="configurationRoot"></param>
    /// <returns></returns>
    private static string GetWinningProvider(IConfigurationRoot configurationRoot)
        => configurationRoot.Providers.LastOrDefault(p => TryGetEnvironmentName(p, out _))?.ToString() ?? "???";

    private static bool TryGetEnvironmentName(IConfigurationProvider provider, out string? value) =>
        provider.TryGet("DOTNET_ENVIRONMENT", out value) ||         //Environment Variable Form
        provider.TryGet("ASPNETCORE_ENVIRONMENT", out value) ||     //Environment Variable Form, for asp.net
        provider.TryGet("environment", out value);                  // CLI form
}