using System.Collections.Generic;
using PaymentGateway.Models.Contracts;

namespace PaymentGateway.Services
{
    public interface IBusinessRulesValidator
    {
        IEnumerable<string> Validate(PaymentContract paymentContract);
    }
}