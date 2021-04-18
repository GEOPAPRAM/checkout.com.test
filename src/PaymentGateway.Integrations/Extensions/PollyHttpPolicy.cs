using System;
using System.Net.Http;
using System.Threading.Tasks;
using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace PaymentGateway.Integrations.Extensions
{
    public static class PollyHttpPolicy
    {
        public static IAsyncPolicy<HttpResponseMessage> Default(int retries = 2, int timeoutSeconds = 3, int circuitBreakerFailures = 5, int circuitBreakerSeconds = 30)
        {
            var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError().RetryAsync(retries, OnRetry);
            var circuitBreakerPolicy = HttpPolicyExtensions.HandleTransientHttpError()
                .Or<TimeoutRejectedException>()
                .CircuitBreakerAsync(circuitBreakerFailures, TimeSpan.FromSeconds(circuitBreakerSeconds));
            var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(timeoutSeconds), TimeoutStrategy.Optimistic);

            return Policy.WrapAsync(retryPolicy, circuitBreakerPolicy, timeoutPolicy);
        }

        public static Task OnRetry(DelegateResult<HttpResponseMessage> result, int retryTime)
        {
            //TODO: Log retries
            return Task.CompletedTask;
        }
    }
}