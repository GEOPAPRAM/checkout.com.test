using System;
using System.Threading.Tasks;
using PaymentGateway.Models.Domain;

namespace PaymentGateway.Services
{
    public interface IPaymentService
    {
        Task<Payment> GetPayment(Guid paymentId);
        Task<(bool Success, Payment Data, string Errors)> MakePayment(Payment payment, Guid merchantId);
    }
}