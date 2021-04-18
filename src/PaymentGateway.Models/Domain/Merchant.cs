using System;

namespace PaymentGateway.Models.Domain
{
    public class Merchant
    {
        public Guid MerchantId { get; set; }
        public string CompanyName { get; set; }
    }
}