using Polly;
using Polly.Extensions.Http;

namespace ContactApp.Shared.HttpServices;

public static class RetryPolicy
{
    public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(1));
    }
}