using System.Threading.Tasks;
using PaymentGateway.Models.Domain;

namespace PaymentGateway.Integrations.Clients
{
    public interface IPaymentProvider
    {
        Task<PaymentProcessResults> ProcessPayment(Payment payment);
    }
}