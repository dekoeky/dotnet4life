using Microsoft.AspNetCore.RateLimiting;

namespace WebApplication1.RateLimiters;

public static class RateLimiterOptionsExtensions
{
    public static RateLimiterOptions SingleConcurrentRequest(this RateLimiterOptions options, string policyName)
        => options.AddConcurrencyLimiter(policyName, o =>
        {
            o.PermitLimit = 1;
            o.QueueLimit = 0;
        });
}