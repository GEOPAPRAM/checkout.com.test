using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.DataAccess;
using PaymentGateway.Services;

namespace PaymentGateway.DependencyInjection
{
    public static class PaymentGatewayServices
    {
        public static void AddPaymentGatewayDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DbOptions>(configuration);

            services.AddTransient<IPaymentRepository, PaymentRepository>();
            services.AddTransient<IPaymentService, PaymentService>();
        }
    }
}