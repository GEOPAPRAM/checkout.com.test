using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PaymentGateway.Integrations.Clients;

namespace PaymentGateway.Integrations.Extensions
{
    public static class IntegrationsServiceCollectionExtension
    {
        public static IServiceCollection AddIntegrations(this IServiceCollection services, ILogger logger)
        {
            services.AddHttpClient(nameof(LehmanSistersApiClient)).AddPolicyHandler(PollyHttpPolicy.Default(timeoutSeconds: 15));
            services.AddHttpClient(nameof(DebitSuissApiClient)).AddPolicyHandler(PollyHttpPolicy.Default(timeoutSeconds: 60));
            return services;
        }
    }
}