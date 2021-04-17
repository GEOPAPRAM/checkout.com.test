using System;
using System.Threading.Tasks;
using PaymentGateway.Domain;

namespace PaymentGateway.DataAccess
{
    public interface IPaymentRepository
    {
        Task<Payment> Get(Guid paymentId);
        Task Create(Payment payment);
    }
}