using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Integrations.Clients;

namespace PaymentGateway.Integrations.Extensions
{
    public static class IntegrationsServiceCollectionExtension
    {
        public static IServiceCollection AddIntegrations(this IServiceCollection services)
        {
            services.AddHttpClient(nameof(LehmanSistersPaymentProvider)).AddPolicyHandler(PollyHttpPolicy.Default(timeoutSeconds: 15));
            services.AddHttpClient(nameof(DebitSuissePaymentProvider)).AddPolicyHandler(PollyHttpPolicy.Default(timeoutSeconds: 60));
            return services;
        }
    }
}