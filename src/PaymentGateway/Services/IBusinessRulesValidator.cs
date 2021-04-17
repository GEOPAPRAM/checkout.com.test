using System.Collections.Generic;
using PaymentGateway.Domain;

namespace PaymentGateway.Services
{
    public interface IBusinessRulesValidator
    {
        IEnumerable<string> Validate(Payment payment);
    }
}