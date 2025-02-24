using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using WebApplication1.Json.Modifiers;

namespace WebApplication1.Configuration;

internal static class Json
{
    /// <summary>
    /// Application specific configuration for Json Serialization on Controllers.
    /// </summary>
    public static void Configure(Microsoft.AspNetCore.Mvc.JsonOptions options)
    {
        ConfigureSharedOptions(options.JsonSerializerOptions);
    }


    /// <summary>
    /// Application specific configuration for Json Serialization on Minimal Apis.
    /// </summary>
    public static void Configure(Microsoft.AspNetCore.Http.Json.JsonOptions options)
    {
        ConfigureSharedOptions(options.SerializerOptions);

        //Just to show that we can use different options for the minimal api endpoints and controllers
        options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseUpper;
    }


    /// <summary>
    /// Centralized json configuration for both Controllers and Minimal Apis.
    /// </summary>
    /// <param name="options"></param>
    private static void ConfigureSharedOptions(JsonSerializerOptions options)
    {
        options.WriteIndented = true;
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

        options.TypeInfoResolver = new DefaultJsonTypeInfoResolver
        {
            Modifiers =
            {
                MyModifiers.AddTypeName,
            }
        };

        options.Converters.Add(new JsonStringEnumConverter());
    }
}
