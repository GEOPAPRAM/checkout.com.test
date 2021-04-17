using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Contracts;
using PaymentGateway.DataAccess;
using PaymentGateway.Domain;
using PaymentGateway.Enums;

namespace PaymentGateway.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IBusinessRulesValidator _businessRulesValidator;

        public PaymentService(IPaymentRepository paymentRepository,
                              IBusinessRulesValidator businessRulesValidator)
        {
            _businessRulesValidator = businessRulesValidator;
            _paymentRepository = paymentRepository;
        }
        public async Task<PaymentContract> GetPayment(Guid paymentId)
        {
            var payment = await _paymentRepository.Get(paymentId);
            return payment == null ? default(PaymentContract) : ToContract(payment);
        }

        public async Task<(bool Success, PaymentContract Data, string Errors)> MakePayment(PaymentContract paymentContract)
        {
            var payment = ToDomain(paymentContract);
            var validationErrors = string.Join("\n", _businessRulesValidator.Validate(payment));

            if (validationErrors.Length > 0)
                return (false, paymentContract, validationErrors);
            //TODO: Implement the following steps
            // 1. Get merchant object
            // 2. Create payment object with Pending status - OK

            await _paymentRepository.Create(payment);
            // 3. Perofm payment transaction with the bank API
            // 4. Update payment record with the result - success/faild
            // 5. Return payment contract

            return (true, ToContract(payment), string.Empty);
        }

        private PaymentContract ToContract(Payment payment)
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

        private Payment ToDomain(PaymentContract paymentContract)
        {
            return new Payment
            {
                PaymentId = Guid.NewGuid(),
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