using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace PaymentGateway.Integrations.Clients
{
    public class DebitSuissApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public DebitSuissApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(DebitSuissApiClient));
        }
    }
}