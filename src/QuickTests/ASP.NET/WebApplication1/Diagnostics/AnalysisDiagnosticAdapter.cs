using Microsoft.Extensions.DiagnosticAdapter;

namespace WebApplication1.Diagnostics;

//requires Microsoft.AspNetCore.MiddlewareAnalysis
//requires Microsoft.Extensions.DiagnosticAdapter

public class AnalysisDiagnosticAdapter(ILogger<AnalysisDiagnosticAdapter> logger)
{
    [DiagnosticName("Microsoft.AspNetCore.MiddlewareAnalysis.MiddlewareStarting")]
    public void OnMiddlewareStarting(HttpContext httpContext, string name, Guid instance, long timestamp)
    {
        logger.LogInformation("MiddlewareStarting: '{name}'; Request Path: '{HttpContext.Request.Path}'", name, httpContext.Request.Path);
    }

    [DiagnosticName("Microsoft.AspNetCore.MiddlewareAnalysis.MiddlewareException")]
    public void OnMiddlewareException(Exception exception, HttpContext httpContext, string name, Guid instance, long timestamp, long duration)
    {
        logger.LogError(exception, "MiddlewareException: '{name}'; '{exceptionMessage}'", name, exception.Message);
    }

    [DiagnosticName("Microsoft.AspNetCore.MiddlewareAnalysis.MiddlewareFinished")]
    public void OnMiddlewareFinished(HttpContext httpContext, string name, Guid instance, long timestamp, long duration)
    {
        logger.LogInformation("MiddlewareFinished: '{name}'; Status: '{httpContext.Response.StatusCode}'", name, httpContext.Response.StatusCode);
    }
}