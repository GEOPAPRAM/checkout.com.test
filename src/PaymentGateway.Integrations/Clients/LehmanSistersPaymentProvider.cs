using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PaymentGateway.Integrations.Extensions;
using PaymentGateway.Models.Domain;

namespace PaymentGateway.Integrations.Clients
{
    public class LehmanSistersPaymentProvider : IPaymentProvider
    {
        private readonly HttpClient _httpClient;
        private readonly IntegrationOptions _options;
        public LehmanSistersPaymentProvider(IHttpClientFactory httpClientFactory, IOptions<IntegrationOptions> configuration)
        {
            _options = configuration.Value;
            _httpClient = httpClientFactory.CreateClient(nameof(LehmanSistersPaymentProvider));
            _httpClient.BaseAddress = new Uri(_options.INTEGRATION_LEHMANSISTERS_BASEURL);
        }

        public async Task<PaymentProcessResults> ProcessPayment(Payment payment)
        {
            return await Task.FromResult(new PaymentProcessResults
            {
                Success = true,
                TranscactionIdentifier = "lm-111111111111111-3003030"
            });
        }
    }
}