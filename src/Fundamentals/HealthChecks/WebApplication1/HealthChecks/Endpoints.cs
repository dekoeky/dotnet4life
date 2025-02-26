namespace WebApplication1.HealthChecks;

public static class Endpoints
{
    public const string DefaultRoot = "health";
    public const string All = $"{DefaultRoot}/all";
    public const string Live = $"{DefaultRoot}/live";
    public const string Ready = $"{DefaultRoot}/ready";
    public const string ExplainJson = $"{DefaultRoot}/explain";
}
