using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PaymentGateway.Integrations.Extensions;
using PaymentGateway.Models.Domain;

namespace PaymentGateway.Integrations.Clients
{
    public class DebitSuissePaymentProvider : IPaymentProvider
    {
        private readonly HttpClient _httpClient;
        private readonly IntegrationOptions _options;
        public DebitSuissePaymentProvider(IHttpClientFactory httpClientFactory, IOptions<IntegrationOptions> configuration)
        {
            _options = configuration.Value;
            _httpClient = httpClientFactory.CreateClient(nameof(DebitSuissePaymentProvider));
            _httpClient.BaseAddress = new Uri(_options.INTEGRATION_DEBITSUISSE_BASEURL);
        }

        public async Task<PaymentProcessResults> ProcessPayment(Payment payment)
        {
            return await Task.FromResult(new PaymentProcessResults
            {
                Success = true,
                TranscactionIdentifier = "ds-222222222222222-6003312030-abc"
            });
        }
    }
}