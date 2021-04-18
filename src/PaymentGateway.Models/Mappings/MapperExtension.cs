using System;
using PaymentGateway.Models.Contracts;
using PaymentGateway.Models.Domain;
using PaymentGateway.Models.Enums;

namespace PaymentGateway.Models.Mappings
{
    public static class MapperExtension
    {
        public static PaymentContract ToContract(this Domain.Payment payment)
        {
            return new PaymentContract
            {
                Id = payment.PaymentId,
                Date = payment.Date,
                Amount = payment.Amount,
                Currency = payment.Currency,
                CardNumber = payment.CardNumber,
                CVV = payment.CVV.ToString(),
                ExpiryMonth = payment.ExpiryMonth,
                ExpiryYear = payment.ExpiryYear
            };
        }

        public static Payment ToDomain(this PaymentContract paymentContract, Guid? paymentId = default(Guid?))
        {
            return new Payment
            {
                PaymentId = paymentId.HasValue ? paymentId.Value : Guid.NewGuid(),
                Date = DateTime.UtcNow,
                Amount = paymentContract.Amount,
                Currency = paymentContract.Currency,
                CardNumber = paymentContract.CardNumber,
                CVV = int.Parse(paymentContract.CVV),
                ExpiryMonth = paymentContract.ExpiryMonth,
                ExpiryYear = paymentContract.ExpiryYear,
                Status = PaymentStatus.Pending
            };
        }
    }
}