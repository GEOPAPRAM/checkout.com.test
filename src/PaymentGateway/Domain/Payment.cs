using System;
using PaymentGateway.Enums;

namespace PaymentGateway.Domain
{
    public class Payment
    {
        public Guid PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public Merchant Merchant { get; set; }
        public string Number { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public int CVV { get; set; }
    }
}