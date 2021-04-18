using System.Collections.Generic;
using PaymentGateway.Models.Domain;

namespace PaymentGateway.Services
{
    public interface IBusinessRulesValidator
    {
        IEnumerable<string> Validate(Payment payment);
    }
}