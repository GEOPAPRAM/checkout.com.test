using System;
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

        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }
        public async Task<PaymentContract> GetPayment(Guid paymentId)
        {
            var payment = await _paymentRepository.Get(paymentId);
            return payment == null ? default(PaymentContract) : ToContract(payment);
        }

        public async Task<(bool, PaymentContract, string)> MakePayment(PaymentContract paymentContract)
        {
            //TODO: Implement the following steps
            // 1. Get merchant object
            // 2. Create payment object with Pending status - OK
            var payment = ToDomain(paymentContract);
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