using System;
using System.Collections.Generic;
using PaymentGateway.Models.Domain;

namespace PaymentGateway.Services
{
    public class BusinessRulesValidator : IBusinessRulesValidator
    {
        public IEnumerable<string> Validate(Payment payment)
        {
            //Check card expiry date
            if (payment.ExpiryYear < DateTime.UtcNow.Year || payment.ExpiryMonth < DateTime.UtcNow.Month)
                yield return "The card has expired";

            //Check the amount should not be negative
            if (payment.Amount < 0)
                yield return "Amount cannot be negative";

            //TODO: Here we keep it simple, but we may want to validate other business 
            // rules, like for instance different maximum amount per currency etc.
            // All such checks and limitations can be implemented as buseness rules.
            // If rules become complex a new BusinessRule class can be introduced.
        }
    }
}