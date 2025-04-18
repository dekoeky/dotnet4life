namespace http_echo;

public static class Endpoints
{
    public static string EchoMethodOnPath(HttpContext httpContext) => $"{httpContext.Request.Method} on {httpContext.Request.Path}";
}