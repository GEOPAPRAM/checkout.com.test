using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PaymentGateway.Integrations.Exceptions;

namespace PaymentGateway.Integrations.Clients
{
    public class PaymentProviderFactory : IPaymentProviderFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public PaymentProviderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IPaymentProvider Create(string providerName) =>

            providerName switch
            {
                "LehmanSisters" => _serviceProvider.GetService<LehmanSistersPaymentProvider>(),
                "DebitSuisse" => _serviceProvider.GetService<DebitSuissePaymentProvider>(),
                _ => throw new PaymentProviderException("Unknow payment provider requested")
            };
    }
}