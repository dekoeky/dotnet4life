using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;

namespace WebApplication1.HealthChecks;

/// <summary>
/// Custom health check response writers.
/// </summary>
public static class HealthCheckResponseWriters
{
    public static async Task WriteExplainResponse(HttpContext context, HealthReport healthReport)
    {
        var options = new JsonWriterOptions { Indented = true };

        context.Response.ContentType = "application/json; charset=utf-8";

        await using var jsonWriter = new Utf8JsonWriter(context.Response.Body, options);
        jsonWriter.WriteStartObject();
        jsonWriter.WriteString("status", healthReport.Status.ToString());
        jsonWriter.WriteStartObject("results");

        foreach (var healthReportEntry in healthReport.Entries)
        {
            jsonWriter.WriteStartObject(healthReportEntry.Key);
            jsonWriter.WriteString("status", healthReportEntry.Value.Status.ToString());
            jsonWriter.WriteString("description", healthReportEntry.Value.Description);
            jsonWriter.WriteStartObject("data");

            foreach (var item in healthReportEntry.Value.Data)
            {
                jsonWriter.WritePropertyName(item.Key);
                JsonSerializer.Serialize(jsonWriter, item.Value, item.Value.GetType());
            }

            jsonWriter.WriteEndObject();
            jsonWriter.WriteEndObject();
        }

        jsonWriter.WriteEndObject();
        jsonWriter.WriteEndObject();

        await jsonWriter.FlushAsync();
    }
}