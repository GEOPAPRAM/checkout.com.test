using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Contracts;

namespace PaymentGateway.Services
{
    public class PaymentService : IPaymentService
    {
        public Task<PaymentContract> GetPayment(Guid paymentId)
        {
            throw new NotImplementedException();
        }

        public Task<ActionResult<PaymentContract>> MakePayment(PaymentContract payment)
        {
            throw new NotImplementedException();
        }
    }
}