using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.DataAccess;
using PaymentGateway.Integrations.Clients;
using PaymentGateway.Integrations.Extensions;
using PaymentGateway.Services;

namespace PaymentGateway.DependencyInjection
{
    public static class PaymentGatewayServices
    {
        public static void AddPaymentGatewayDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DbOptions>(configuration);
            services.Configure<IntegrationOptions>(configuration);

            services.AddSingleton<IPaymentRepository, PaymentRepository>();
            services.AddSingleton<IAccountRepository, AccountRepository>();
            services.AddSingleton<IBusinessRulesValidator, BusinessRulesValidator>();
            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IPaymentProviderFactory, PaymentProviderFactory>();
            services.AddIntegrations();
            services.AddScoped<LehmanSistersPaymentProvider>();
            services.AddScoped<DebitSuissePaymentProvider>();
        }
    }
}