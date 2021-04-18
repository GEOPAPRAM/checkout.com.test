using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace PaymentGateway.Integrations.Clients
{
    public class LehmanSistersApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public LehmanSistersApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(LehmanSistersApiClient));
        }
    }
}