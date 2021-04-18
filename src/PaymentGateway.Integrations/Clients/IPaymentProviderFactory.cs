using System.Threading.Tasks;

namespace PaymentGateway.Integrations.Clients
{
    public interface IPaymentProviderFactory
    {
        IPaymentProvider Create(string providerName);
    }
}