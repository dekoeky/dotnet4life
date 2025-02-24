using Microsoft.AspNetCore.RateLimiting;
using WebApplication1.RateLimiters;

namespace WebApplication1.Configuration;

internal static class RateLimiting
{
    public const string SingleConcurrentRequest = nameof(SingleConcurrentRequest);
    public const string SlidingWindowMaxOneEveryTwoSeconds = nameof(SlidingWindowMaxOneEveryTwoSeconds);

    public static void Configure(RateLimiterOptions options)
    {
        options.RejectionStatusCode = StatusCodes.Status503ServiceUnavailable;
        //options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

        options.SingleConcurrentRequest(SingleConcurrentRequest);
        options.AddSlidingWindowLimiter(SlidingWindowMaxOneEveryTwoSeconds, limiterOptions =>
        {
            limiterOptions.Window = TimeSpan.FromSeconds(2);
            limiterOptions.PermitLimit = 1;
            limiterOptions.QueueLimit = 0;
            limiterOptions.SegmentsPerWindow = 4;
        });

        options.OnRejected = (context, token) =>
        {
            context.HttpContext.Response.Headers["ReceivedAt"] = DateTime.Now.ToString("O");
            return ValueTask.CompletedTask;
        };
    }
}