using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PaymentGateway.Contracts;

namespace PaymentGateway.Services
{
    public interface IPaymentService
    {
        Task<PaymentContract> GetPayment(Guid paymentId);
        Task<ActionResult<PaymentContract>> MakePayment(PaymentContract payment);
    }
}