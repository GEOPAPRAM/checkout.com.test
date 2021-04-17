using System;
using System.Threading.Tasks;
using PaymentGateway.Contracts;

namespace PaymentGateway.Services
{
    public interface IPaymentService
    {
        Task<PaymentContract> GetPayment(Guid paymentId);
        Task<(bool, PaymentContract, string)> MakePayment(PaymentContract payment);
    }
}