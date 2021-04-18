using System;
using System.Threading.Tasks;
using PaymentGateway.Models.Domain;

namespace PaymentGateway.DataAccess
{
    public interface IPaymentRepository
    {
        Task<Payment> Get(Guid paymentId);
        Task Create(Payment payment);
    }
}