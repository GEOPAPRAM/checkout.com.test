using System;
using System.Threading.Tasks;
using PaymentGateway.Models.Contracts;

namespace PaymentGateway.Services
{
    public interface IPaymentService
    {
        Task<PaymentContract> GetPayment(Guid paymentId);
        Task<(bool Success, PaymentContract Data, string Errors)> MakePayment(PaymentContract payment, Guid merchantId);
    }
}