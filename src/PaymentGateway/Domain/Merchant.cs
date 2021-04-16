using System;

namespace PaymentGateway.Domain
{
    public class Merchant
    {
        public Guid MerchantId { get; set; }
        public string CompanyName { get; set; }
    }
}