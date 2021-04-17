using System;
using System.Text.RegularExpressions;
using PaymentGateway.Enums;

namespace PaymentGateway.Domain
{
    public class Payment
    {
        private static Regex Obfuscator = new Regex(@"\d", RegexOptions.Compiled);
        private string _cardNumber;
        public Guid PaymentId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public Merchant Merchant { get; set; }
        public string CardNumber
        {
            get => Obfuscator.Replace(_cardNumber, "X", _cardNumber.Length - 4);
            set { _cardNumber = value; }
        }

        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public int CVV { get; set; }

        public PaymentStatus Status { get; set; }
    }
}