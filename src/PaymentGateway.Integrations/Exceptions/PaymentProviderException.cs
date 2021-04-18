using System;

namespace PaymentGateway.Integrations.Exceptions
{
    public class PaymentProviderException : Exception
    {
        public PaymentProviderException(string message) : base(message) { }
    }
}